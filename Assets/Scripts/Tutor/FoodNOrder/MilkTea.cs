using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MilkTea : MonoBehaviour
{
    #region - Variable -

    public bool IsServable => isServable;
    public Dictionary<string, bool> MilkTeaBaseIngredient => _milkTeaIngredients;
    public Dictionary<string, bool> MilkTeaIngredient => _milkTeaIngredients;

    [Tooltip("No use in script just preview")]
    public string[] recipeIngredientName;

    [SerializeField] private string filledTag;
    [SerializeField] private bool isServable;
    [SerializeField] private MilkTeaType milkTeaType;

    [SerializeField, Tooltip("ล็อกสถานะของเมนูเอาไว้ไม่ให้เปลี่ยนแปลง หากมีการใส่วัตถุดิบเพิ่มเติมในขณะที่มีวัตถุดิบอื่นอยู่แล้ว")] private bool menuLock;

    private Dictionary<string, bool> _milkTeaBaseIngredients = new Dictionary<string, bool>();
    private Dictionary<string, bool> _milkTeaIngredients = new Dictionary<string, bool>();
    private Dictionary<string, GameObject> _milkTeaVisual = new Dictionary<string, GameObject>();

    private bool _menuIsLocked;    

    #endregion

    #region - Unity Event -

    private void Awake()
    {
        foreach (string name in Enum.GetNames(typeof(MilkTeaRecipe.Ingredient)))
        {
            _milkTeaIngredients.Add(name, false);
            try
            {
                Debug.Log(transform.Find(name).gameObject);
                _milkTeaVisual.Add(name, transform.Find(name).gameObject);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        foreach (MilkTeaRecipe.Ingredient baseIngredient in milkTeaType.baseIngredients)
        {
            string ingredientName = baseIngredient.ToString();

            if (_milkTeaIngredients.ContainsKey(ingredientName))
            {
                _milkTeaIngredients.Remove(ingredientName);

                _milkTeaBaseIngredients.Add(ingredientName, false);
            }
        }
    }

    #endregion

    #region - User-Defind -

    public void AddIngredient(MilkTeaRecipe.Ingredient ingredientName)
    {
        string ingredient = ingredientName.ToString();
        
        if (_milkTeaBaseIngredients.ContainsKey(ingredient))
        {
            _milkTeaBaseIngredients[ingredient] = true;
            _milkTeaVisual[ingredient].SetActive(true);
        }
        else if (_milkTeaIngredients.ContainsKey(ingredient))
        {
            _milkTeaIngredients[ingredient] = true;
        }

        UpdateMenuStatus();
    }

    /// <summary>
    /// Function that check every ingredient both base and extra is present or not to determine if the milk tea is servable or not and what is this drink
    /// </summary>
    public void UpdateMenuStatus()
    {
        bool waitForBaseIngredient = true;

        foreach (MilkTeaRecipe.Ingredient ingredient in milkTeaType.baseIngredients)
        {
            if (!waitForBaseIngredient)
            {
                Debug.LogWarning("Base Ingredient is not in order");
                break;
            }

            string ingredientName = ingredient.ToString();

            if (_milkTeaBaseIngredients.ContainsKey(ingredientName))
            {
                waitForBaseIngredient = _milkTeaBaseIngredients[ingredientName];
            }
        }

        if (!waitForBaseIngredient) return;
        

        foreach (MilkTeaRecipe recipe in milkTeaType.recipes)
        {
            if (menuLock && _menuIsLocked)
            {
                Debug.LogWarning($"Menu of '{gameObject.name}' is locked");
                return;
            }
            
            if (!recipe.haveExtraIngredient)
            {
                gameObject.name = recipe.menuName;
                isServable = true;
                Debug.Log("Now It's serveable");
            }

            int ingredientCount = 0;
            int specialIngredientsQuantity = recipe.specialIngredients.Length;

            foreach (MilkTeaRecipe.Ingredient ingredient in recipe.specialIngredients)
            {
                if (_milkTeaIngredients[ingredient.ToString()])
                {
                    ingredientCount++;
                    _milkTeaVisual[ingredient.ToString()].SetActive(true);
                }

                if (ingredientCount == specialIngredientsQuantity)
                {
                    gameObject.name = recipe.menuName;
                    if(menuLock) _menuIsLocked = true;
                }
            }
        }

        Debug.Log($"This drink is {gameObject.name}");
    }

    public void ChangeToFilledTag()
    {
        gameObject.tag = filledTag;
    }

    public bool CheckIngredient(MilkTeaRecipe.Ingredient ingredientName)
    {
        if (_milkTeaBaseIngredients.ContainsKey(ingredientName.ToString()))
        {
            return _milkTeaBaseIngredients[ingredientName.ToString()];
        }

        if (_milkTeaIngredients.ContainsKey(ingredientName.ToString()))
        {
            return _milkTeaIngredients[ingredientName.ToString()];
        }

        return false;
    }

    #endregion

    #region * Debug *

    [ContextMenu("Add/Milk Tea")]
    private void AddTea()
    {
        AddIngredient(MilkTeaRecipe.Ingredient.MilkTea);
    }

    [ContextMenu("Add/Ice")]
    private void AddIce()
    {
        AddIngredient(MilkTeaRecipe.Ingredient.Ice);
    }

    [ContextMenu("Add/Boba")]
    private void AddBoba()
    {
        AddIngredient(MilkTeaRecipe.Ingredient.Boba);
    }

    [ContextMenu("Add/Ice Cream")]
    private void AddIceCream()
    {
        AddIngredient(MilkTeaRecipe.Ingredient.IceCream);
    }

    [ContextMenu("Clear")]
    private void Clear()
    {
        foreach (string name in Enum.GetNames(typeof(MilkTeaRecipe.Ingredient)))
        {
            if (_milkTeaIngredients.ContainsKey(name)) _milkTeaIngredients[name] = false;
            else if (_milkTeaBaseIngredients.ContainsKey(name)) _milkTeaBaseIngredients[name] = false;
        }

        gameObject.name = "Wipe Out";
    }

    [ContextMenu("Debug/Preview Ingredient List")]
    private void PreviewIngredientList()
    {
        recipeIngredientName = Array.Empty<string>();
        recipeIngredientName = Enum.GetNames(typeof(MilkTeaRecipe.Ingredient));
    }

    #endregion
}