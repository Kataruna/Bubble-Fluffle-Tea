using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CabinetDoor : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private CabinetProtocol assignOperator;
    [SerializeField] private UnityEvent OnOpen;

    private float _delayCount;
    private bool _delayEnable;

    private void FixedUpdate()
    {
        if (!_delayEnable) return;

        _delayCount += Time.deltaTime;
        if (_delayCount >= delay)
        {
            Debug.Log("Stop Dely");
            _delayEnable = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RefillLock"))
        {
            assignOperator.CleanCabinet();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        //Debug.LogWarning(other.name);

        if (_delayEnable) return;
        if (other.CompareTag("RefillLock"))
        {
            _delayEnable = true;
            Debug.Log("Start Delay");
            OnOpen.Invoke();
        }
    }

    [ContextMenu("Silly Spawn")]
    private void SillySpawn()
    {
        OnOpen.Invoke();
    }
}
