using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CabinetDoor : MonoBehaviour
{
    [SerializeField] private CabinetProtocol assignOperator;
    [SerializeField] private UnityEvent OnOpen;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RefillLock"))
        {
            assignOperator.CleanCabinet();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RefillLock"))
        {
            OnOpen.Invoke();
        }
    }

    [ContextMenu("Silly Spawn")]
    private void SillySpawn()
    {
        OnOpen.Invoke();
    }
}
