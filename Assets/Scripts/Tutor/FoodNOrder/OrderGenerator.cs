using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrderGenerator : MonoBehaviour
{
    public static OrderGenerator Instance;

    public MilkTeaType Drink => drink;
    
    [SerializeField] private MilkTeaType drink;

    private void Awake()
    {
        Instance = this;
    }

    public Order RandomOrder()
    {
        Order result = new Order();

        result.drinkIndex = Random.Range(0, drink.recipes.Length);
        
        int chance = Random.Range(0, 100);
        if (chance % 2 == 0) result.wantCroissant = true;
        else result.wantCroissant = false;

        result.drinkName = drink.recipes[result.drinkIndex].menuName;
        
        return result;
    }
    
    [ContextMenu("Random Order")]
    private void RandomOrderTest()
    {
        Order order = RandomOrder();
        
        string croissant = order.wantCroissant ? "with croissant" : "without croissant";
        Debug.Log($"Order: {drink.recipes[order.drinkIndex].menuName} {croissant}");
    }
}

[Serializable]
public class Order
{
    public int drinkIndex;
    public string drinkName;
    public bool wantCroissant;

    public bool drinkIsServed = false;
    public bool croissantIsServed = false;
}
