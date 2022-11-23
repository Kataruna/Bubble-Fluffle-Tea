using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class FeedbacksManager : MonoBehaviour
{

    public static FeedbacksManager Instance;
    [Header("Feedbacks")]
    [SerializeField] private MMFeedbacks itemInteractFeedback;
    [SerializeField] private MMFeedbacks bobaInteractFeedback;
    [SerializeField] private MMFeedbacks iceInteractFeedback;
    [SerializeField] private MMFeedbacks completeMenuFeedback;
    [SerializeField] private MMFeedbacks rewardFeedback;
    [SerializeField] private MMFeedbacks teaFeedback;
    [SerializeField] private MMFeedbacks customersInFeedback;



    public MMFeedbacks ItemInteractFeedback => itemInteractFeedback;
    public MMFeedbacks BobaInteractFeedback => bobaInteractFeedback;
    public MMFeedbacks IceInteractFeedback => iceInteractFeedback;
    public MMFeedbacks CompleteMenuFeedback => completeMenuFeedback;
    public MMFeedbacks RewardFeedback => rewardFeedback;
    public MMFeedbacks TeaFeedback => teaFeedback;
    public MMFeedbacks CustomersInFeedback => customersInFeedback;

    private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
}
