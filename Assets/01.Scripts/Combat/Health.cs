using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    private ActionData _actionData;

    public int maxHealth;
    public int currentHealth;

    public bool IsDead;

    private void Awake()
    {
        _actionData = GetComponent<ActionData>();

        IsDead = false;
        currentHealth = maxHealth; //나중에 SO로 뺄 것
    }

    public void ApplyDamage(int damage, Vector3 point, Vector3 normal)
    {
        if (IsDead) return;

        currentHealth -= damage;
        _actionData.HitPoint = point;
        _actionData.HitNormal = normal;

        Debug.Log("Hit!");
    }
}
