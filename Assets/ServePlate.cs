using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class ServePlate : MonoBehaviour
{
    private CustomerService _customerService;
    
    [SerializeField] private int plateId;
    
    private void Awake()
    {
        _customerService = CustomerService.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Glass"))
            if(!other.GetComponent<MilkTea>().IsServable) return;

        Customer customer = _customerService.SpaceId[plateId].customerSlot;
        
        if (other.name == customer.order.drinkName)
        {
            customer.order.drinkIsServed = true;
            other.GetComponent<Rigidbody>().isKinematic = true;
            customer.Hold(other.gameObject.transform);
        }
        else if (customer.order.wantCroissant && other.name == "Croissant")
        {
            customer.order.croissantIsServed = true;
            other.GetComponent<Rigidbody>().isKinematic = true;
            customer.Hold(other.gameObject.transform);
        }
        
        customer.CheckOrder();
    }
}
