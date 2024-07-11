using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class TimeManager : MonoSingleton<TimeManager>
{
    [SerializeField]
    private float _stopDealy = 0.5f;

    public void StopTime(float timeScale)
    {
        Time.timeScale = timeScale;
        CoroutineUtil.CallWaitForSeconds(_stopDealy, Resume);
    }

    private void Resume()
    {
        Time.timeScale = 1f;
    }
}
