using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDispenser : MonoBehaviour
{
    [SerializeField] private string tagToCheck;
    [SerializeField] private float distance;

    public void Check()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit, distance))
        {
            // Debug.Log($"Name: {hit.transform.name}");
            // Debug.Log($"Tag: {hit.transform.tag}");
            
            if (hit.transform.CompareTag(tagToCheck))
            {
                // Debug.Log("Passed");
                hit.transform.TryGetComponent(out MilkTea milkTea);

                if (milkTea == null || milkTea.CheckIngredient(MilkTeaRecipe.Ingredient.MilkTea))
                {
                    Debug.Log("MilkTea Already exist so move on.");
                    return;
                }

                FeedbacksManager.Instance.TeaFeedback.PlayFeedbacks();
                
                milkTea.AddIngredient(MilkTeaRecipe.Ingredient.MilkTea);
                milkTea.ChangeToFilledTag();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * distance);
    }
}
