using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class Customer : MonoBehaviour
{
    public Transform HolderOne => holderOne;
    public Transform HolderTwo => holderTwo;
    
    public bool HolderOneIsFree => _holderOneIsFree;
    public bool HolderTwoIsFree => _holderTwoIsFree;

    public Order order;
    public int spaceIndex = -1;
    public CanvasGroup canvasGroup;
    
    [SerializeField] private Transform holderOne;
    [SerializeField] private Transform holderTwo;
    //[SerializeField] private Animator animation;
    
    private bool _holderOneIsFree = true;
    private bool _holderTwoIsFree = true;
    
    private static readonly int Up = Animator.StringToHash("Up");
    private static readonly int Down = Animator.StringToHash("Down");

    public enum Animation
    {
        Up,
        Down
    }
    
    public void Move(Animation stage, Transform targetPosition)
    {
        switch (stage)
        {
            case Animation.Up:
                transform.DOMove(targetPosition.position, 1f);
                break;
            case Animation.Down:    
                transform.DOMove(targetPosition.position, 1f);
                break;
        }
    }

    public void Hold(Transform obj)
    {
        FeedbacksManager.Instance.CompleteMenuFeedback.PlayFeedbacks();
        
        if (_holderOneIsFree)
        {
            obj.SetParent(holderOne);
            
            obj.transform.DOLocalMove(Vector3.zero, 1f);
        }
        else if(_holderTwoIsFree)
        {
            obj.SetParent(holderTwo);
            
            obj.transform.DOLocalMove(Vector3.zero, 1f);
        }
    }

    public void CustomerSetup()
    {
        int tempOne = holderOne.childCount;
        int tempTwo = holderTwo.childCount;

        Debug.LogWarning(tempOne);
        Debug.LogWarning(tempTwo);

        if(tempOne > 0)
            for (int i = tempOne - 1; i >= 0; i--)
            {
                Destroy(holderOne.GetChild(i).gameObject);
            }
        
        if(tempTwo > 0)
            for (int i = tempTwo - 1; i >= 0; i--)
            {
                Destroy(holderTwo.GetChild(i).gameObject);
            }
    }
    
    public void CheckOrder()
    {
        if ((order.wantCroissant && order.croissantIsServed) && order.drinkIsServed)
        {
            CustomerService.Instance.OnCustomerExit(this);
        }
        else if (!order.wantCroissant && order.drinkIsServed)
        {
            CustomerService.Instance.OnCustomerExit(this);
        }
    }
}
