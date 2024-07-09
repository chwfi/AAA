using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : PoolableMono
{
    [SerializeField] private List<ParticleSystem> _particles;
    [SerializeField] private float _endTime;

    private Coroutine _timerCoroutine;

    public void StartPlay()
    {
        if (_particles != null)
            _particles.ForEach(p => p.Play());

        if (_timerCoroutine != null)
            StopCoroutine(_timerCoroutine);

        _timerCoroutine = StartCoroutine(Timer(_endTime));
    }

    protected virtual IEnumerator Timer(float timer)
    {
        yield return new WaitForSeconds(timer);
        PoolManager.Instance.Push(this);
    }
}
