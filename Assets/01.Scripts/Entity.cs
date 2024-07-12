using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Health HealthCompo { get; private set; }
    public AttackableEntity AttackCompo { get; private set; }
    public Dictionary<FeedbackTypeEnum, Feedback> FeedbackDictionary { get; private set; }

    private Feedback[] feedbacks;

    protected virtual void Awake()
    {
        HealthCompo = GetComponent<Health>();
        AttackCompo = GetComponent<AttackableEntity>();
        HealthCompo.SetOwner(this);

        FeedbackDictionary = new();

        Transform feedbackTrm = transform.Find("Feedback").transform;
        feedbacks = feedbackTrm.GetComponents<Feedback>();
        foreach (Feedback feedback in feedbacks)
        {
            feedback.SetOwner(this);
            FeedbackDictionary.Add(feedback.FeedbackType, feedback);
        }
    }
}
