using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Master class that contain all type of Milk Tea Recipe
/// </summary>
[CreateAssetMenu(menuName = "Milk Tea/Type")]
public class MilkTeaType : ScriptableObject
{
    [Tooltip("เมนูชานมทั้งหมดที่มีภายในเกม")] public MilkTeaRecipe[] recipes;
    [Tooltip("วัตถุดิบพื้นฐานสำหรับเมนูชานมทั้งหมด")] public MilkTeaRecipe.Ingredient[] baseIngredients;
}
