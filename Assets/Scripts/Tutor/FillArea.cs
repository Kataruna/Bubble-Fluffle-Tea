using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillArea : MonoBehaviour
{
    [SerializeField] MilkTeaRecipe.Ingredient ingredientToFill;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Glass") || other.CompareTag("Unfill"))
        {
            other.GetComponent<MilkTea>().AddIngredient(ingredientToFill);
        }
    }
}
