using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrderGenerator : MonoBehaviour
{
    [SerializeField] private MilkTeaType drink;

    public Order RandomOrder()
    {
        Order result = new Order();

        result.drinkIndex = Random.Range(0, drink.recipes.Length);
        
        int chance = Random.Range(0, 100);
        if (chance % 2 == 0) result.wantCroissant = true;
        else result.wantCroissant = false;

        return result;
    }
}

[Serializable]
public class Order
{
    public int drinkIndex;
    public bool wantCroissant;
}
