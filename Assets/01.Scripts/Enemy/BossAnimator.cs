using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimator : MonoBehaviour
{
    private Animator _animator;

    private readonly int _speedAnim = Animator.StringToHash("Speed");
    private readonly int _motionSpeedAnim = Animator.StringToHash("MotionSpeed");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}
