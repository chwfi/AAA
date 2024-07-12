using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    private Entity _owner;

    public int maxHealth;
    public int currentHealth;
    public bool IsDead;

    public Vector3 hitPoint;
    public HitTypeEnum hitType;

    private void Awake()
    {
        IsDead = false;
        currentHealth = maxHealth; //나중에 SO로 뺄 것
    }

    public void SetOwner(Entity owner)
    {
        _owner = owner;
    }

    public void ApplyDamage(int damage, Vector3 point, Vector3 normal, HitTypeEnum hitType)
    {
        if (IsDead) return;
         
        SaveHitData(point, hitType);
        currentHealth -= damage;
        _owner.FeedbackDictionary.TryGetValue(FeedbackTypeEnum.Hit, out Feedback feedback);
        feedback.StartFeedback();
        TimeManager.Instance.StopTime(0.5f);

        Debug.Log("Hit!");
    }

    public void SaveHitData(Vector3 point, HitTypeEnum type)
    {
        hitPoint = point;
        hitType = type;
    }
}
