using System;
using System.Collections;
using System.Collections.Generic;
using KevinCastejon.MoreAttributes;
using UnityEngine;

public class LineCheck : MonoBehaviour
{
    [SerializeField] private string tagToCheck;
    [SerializeField] private float distance;

    public void Check()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit, distance))
        {
            Debug.Log($"Name: {hit.transform.name}");
            Debug.Log($"Tag: {hit.transform.tag}");
            
            if (hit.transform.CompareTag(tagToCheck))
            {
                Debug.Log("Passed");
                
                hit.transform.GetComponent<Filler>().Fill();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * distance);
    }
}
