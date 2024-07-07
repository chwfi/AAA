using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Health HealthCompo { get; private set; }
    public ActionData ActionDataCompo { get; private set; }

    protected virtual void Awake()
    {
        HealthCompo = GetComponent<Health>();
        ActionDataCompo = GetComponent<ActionData>();
    }
}
