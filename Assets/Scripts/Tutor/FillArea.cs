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
            switch (ingredientToFill)
            {
                case MilkTeaRecipe.Ingredient.Boba:
                    FeedbacksManager.Instance.BobaInteractFeedback.PlayFeedbacks();
                    break;
                case MilkTeaRecipe.Ingredient.Ice:
                    FeedbacksManager.Instance.IceInteractFeedback.PlayFeedbacks();
                    break;
                case MilkTeaRecipe.Ingredient.IceCream:
                    FeedbacksManager.Instance.IceInteractFeedback.PlayFeedbacks();
                    break;
            }
            other.GetComponent<MilkTea>().AddIngredient(ingredientToFill);
        }
    }
}
