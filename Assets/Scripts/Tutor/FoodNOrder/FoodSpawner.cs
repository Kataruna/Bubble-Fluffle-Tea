using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnedPrefab;

    private GameObject _newestOne;
    
    private void Start()
    {
        Spawn();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _newestOne)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        _newestOne = Instantiate(spawnedPrefab, transform.position, Quaternion.identity);
        
        bool pass = _newestOne.TryGetComponent(out XRGrabInteractable grabInteractable);

        if (pass)
        {
            grabInteractable.interactionManager = Address.Instance.MembersName[Address.Specify.XRInteractionManager]
                .GetComponent<XRInteractionManager>();
        }
    }
}
