using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filler : MonoBehaviour
{
    [SerializeField] private GameObject[] fillers;
    [SerializeField] private string tag;

    public void Fill()
    {
        for (int i = 0; i < fillers.Length; i++)
        {
            fillers[i].SetActive(true);
        }

        transform.tag = tag;
    }
}
