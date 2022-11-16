using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetProtocol : MonoBehaviour
{
    [SerializeField] private List<GameObject> items;

    private void OnTriggerEnter(Collider other)
    {
        items.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        items.Remove(other.gameObject);
    }

    public void CleanCabinet()
    {
        foreach (GameObject item in items)
        {
            Destroy(item);
        }
        
        items.Clear();
    }
}
