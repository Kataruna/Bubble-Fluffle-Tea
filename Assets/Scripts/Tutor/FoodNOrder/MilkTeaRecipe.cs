using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Serialization;

/// <summary>
/// Sub-Class that define what special about each recipe of Milk Tea
/// </summary>
[Serializable]
public class MilkTeaRecipe
{
    [FormerlySerializedAs("menuSprite")] [Tooltip("ภาพเมนู")] public Sprite menuIcon;
    [Tooltip("ชื่อของเมนู")] public string menuName;
    [Tooltip("มีวัตถุดิบเพิ่มเติมหรือไม่")] public bool haveExtraIngredient;
    [Tooltip("วัตถุดิบเสริมพิเศษที่เจาะจงเฉพาะในแก้วนั้น")] public Ingredient[] specialIngredients;
    
    public enum Ingredient
    {
        Ice,
        MilkTea,
        Boba,
        IceCream,
    }
}
