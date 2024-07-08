using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Health HealthCompo { get; private set; }

    protected virtual void Awake()
    {
        HealthCompo = GetComponent<Health>();
    }
}
