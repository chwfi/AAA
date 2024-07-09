using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feedback : MonoBehaviour
{
    protected Entity _owner;

    public void SetOwner(Entity owner)
    {
        _owner = owner;
    }
}
