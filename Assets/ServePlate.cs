using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

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
            
            customer.canvasGroup.transform.GetChild(2).GetComponent<Image>().color = Color.white;
            
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.GetComponent<XRGrabInteractable>().enabled = false;
            customer.Hold(other.gameObject.transform);
        }
        else if (customer.order.wantCroissant && other.name == "Croissant")
        {
            customer.order.croissantIsServed = true;
            
            customer.canvasGroup.transform.GetChild(3).GetComponent<Image>().color = Color.white;
            
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.GetComponent<XRGrabInteractable>().enabled = false;
            customer.Hold(other.gameObject.transform);
        }
        
        customer.CheckOrder();
    }
}
