using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public GameObject MainCam;

    private void Awake()
    {
        if (MainCam == null)
        {
            MainCam = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
