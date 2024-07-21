using UnityEngine;

public enum FeedbackTypeEnum
{
    Hit,
    Attack, 
    Effect,
}

public class Feedback : MonoBehaviour
{
    public FeedbackTypeEnum FeedbackType;

    protected Entity _owner;

    public virtual void SetOwner(Entity owner)
    {
        _owner = owner;
    }

    public virtual void StartFeedback()
    {

    }

    public virtual void StopFeedback()
    {

    }
}
