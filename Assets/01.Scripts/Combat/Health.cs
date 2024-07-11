using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    private CombatFeedback _hitFeedback;

    public int maxHealth;
    public int currentHealth;

    public bool IsDead;

    private void Awake()
    {
        Transform feedbackTrm = transform.Find("Feedback").transform;
        _hitFeedback = feedbackTrm.GetComponent<CombatFeedback>();
        _hitFeedback.SetOwner(transform.GetComponent<Entity>());

        IsDead = false;
        currentHealth = maxHealth; //나중에 SO로 뺄 것
    }

    public void ApplyDamage(int damage, Vector3 point, Vector3 normal, HitTypeEnum hitType)
    {
        if (IsDead) return;

        currentHealth -= damage;
        _hitFeedback.ApplyFeedback(point, hitType);
        TimeManager.Instance.StopTime(0.5f);

        Debug.Log("Hit!");
    }
}
