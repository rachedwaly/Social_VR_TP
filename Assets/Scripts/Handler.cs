using OpenCvSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handler : MonoBehaviour
{
    public static Action onScreenShotTaken;
    public static Action takeSelfie;
    public static Action removeUser;

    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.One))
        {
            takeSelfie?.Invoke();
        }

        if (OVRInput.Get(OVRInput.Button.Two))
        {
            removeUser?.Invoke();
        }

    }
}
