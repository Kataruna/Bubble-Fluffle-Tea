using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MilkTea : MonoBehaviour
{
    #region - Variable -

    public bool IsServable => isServable;

    [SerializeField] private bool isServable;
    [SerializeField] private MilkTeaType milkTeaType;
    
    private Dictionary<string, bool> _milkTeaBaseIngredients = new Dictionary<string, bool>();
    private Dictionary<string, bool> _milkTeaIngredients = new Dictionary<string, bool>();

    #endregion

    #region - Unity Event -

    private void Awake()
    {
        foreach (string name in Enum.GetNames(typeof(MilkTeaRecipe.Ingredient)))
        {
            _milkTeaIngredients.Add(name, false);
        }

        foreach (MilkTeaRecipe.Ingredient baseIngredient in milkTeaType.baseIngredients)
        {
            string ingredientName = baseIngredient.ToString();
            
            if(_milkTeaIngredients.ContainsKey(ingredientName))
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
        if (_milkTeaBaseIngredients.ContainsKey(ingredientName.ToString()))
        {
            _milkTeaBaseIngredients[ingredientName.ToString()] = true;
        }
        else if (_milkTeaIngredients.ContainsKey(ingredientName.ToString()))
        {
            _milkTeaIngredients[ingredientName.ToString()] = true;
        }

        UpdateMenuStatus();
    }

    public void UpdateMenuStatus()
    {
        bool menuComplete = false;
        bool waitForBaseIngredient = true;

        foreach (MilkTeaRecipe.Ingredient ingredient in milkTeaType.baseIngredients)
        {
            if (!waitForBaseIngredient)
            {
                Debug.LogWarning("Base Ingredient is not in order");
                break;
            }
            
            string ingredientName = ingredient.ToString();
            
            if (_milkTeaBaseIngredients.ContainsKey(ingredientName)) waitForBaseIngredient = _milkTeaBaseIngredients[ingredientName];
        }
        
        if(!waitForBaseIngredient) return;

        foreach (MilkTeaRecipe recipe in milkTeaType.recipes)
        {
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
                }

                if (ingredientCount >= specialIngredientsQuantity)
                {
                    gameObject.name = recipe.menuName;
                }
            }
        }

        Debug.Log($"This drink is {gameObject.name}");
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
            if(_milkTeaIngredients.ContainsKey(name)) _milkTeaIngredients[name] = false;
            else if(_milkTeaBaseIngredients.ContainsKey(name)) _milkTeaBaseIngredients[name] = false;
        }

        gameObject.name = "Wipe Out";
    }

    #endregion
}