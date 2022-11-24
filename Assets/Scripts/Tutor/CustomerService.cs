using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CustomerService : MonoBehaviour
{
    public static CustomerService Instance;

    public SpaceProperties[] SpaceId => space;
    
    [SerializeField] private Transform referencePoint;
    [SerializeField] private Color alphaWhite = Color.white;
    
    [SerializeField] private SpaceProperties[] space;
    [SerializeField] private CanvasGroup[] ordersDisplay;
    [SerializeField] private List<Customer> customers;
    
    Queue<Customer> customerQueue = new Queue<Customer>();

    private void Awake()
    {
        Instance = this;

        if(space.Length > customers.Count) Debug.LogError("Not enough space for all customers");
        
        foreach (Customer customer in customers)
        {
            customerQueue.Enqueue(customer);
        }
    }

    private void Start()
    {
        for (int i = 0; i < space.Length; i++)
        {
            OnCustomerEnter(i);
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < space.Length; i++)
        {
            if (!space[i].isOccupied)
            {
                OnCustomerEnter(i);
            }
        }
    }

    public void OnCustomerEnter(int index)
    {
        SpaceProperties space = this.space[index];
        
        space.isOccupied = true;
        
        Customer customer = customerQueue.Dequeue();
        space.customerSlot = customer;
        Debug.Log($"Customer is {customer.gameObject.name}");
        
        customer.spaceIndex = index;
        Debug.Log($"change {customer.transform.position} to {space.space.position}");

        customer.Move(Customer.Animation.Up, space.space);
        
        FeedbacksManager.Instance.CustomersInFeedback.PlayFeedbacks();

        customer.order = OrderGenerator.Instance.RandomOrder();
        customer.canvasGroup = ordersDisplay[index];
        
        DisplayOrder(index, customer.order);
        
    }
    
    public void OnCustomerExit(Customer customer)
    {
        FeedbacksManager.Instance.RewardFeedback.PlayFeedbacks();
        
        customer.order = null;

        int tempIndex = customer.spaceIndex;
        
        customer.canvasGroup = null;
        
        ClearOrderDisplay(tempIndex);

        space[tempIndex].isOccupied = false;
        space[tempIndex].customerSlot = null;
        
        customer.spaceIndex = -1;
        customer.Move(Customer.Animation.Down, referencePoint);

        customerQueue.Enqueue(customer);
        
        OnCustomerEnter(tempIndex);

        StartCoroutine(CooldownReset(customer));
    }

    private void DisplayOrder(int index, Order order)
    {
        ordersDisplay[index].transform.GetChild(0).GetComponent<Image>().sprite = OrderGenerator.Instance.Drink.recipes[order.drinkIndex].menuIcon;
        
        if(order.wantCroissant) ordersDisplay[index].transform.GetChild(1).gameObject.SetActive(true);
    }

    private void ClearOrderDisplay(int index)
    {
        ordersDisplay[index].transform.GetChild(0).GetComponent<Image>().sprite = null;
        ordersDisplay[index].transform.GetChild(1).gameObject.SetActive(false);
        
        ordersDisplay[index].transform.GetChild(2).GetComponent<Image>().color = alphaWhite;
        ordersDisplay[index].transform.GetChild(3).GetComponent<Image>().color = alphaWhite;
    }

    IEnumerator CooldownReset(Customer customer)
    {
        yield return new WaitForSeconds(5f);
        customer.CustomerSetup();
    }
}

[Serializable]
public class SpaceProperties
{
    public Transform space;
    public bool isOccupied;
    public Customer customerSlot;
}