using UnityEngine;
using System;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    private void Awake()
    {
        instance = this;
    }

    public event Action<float, float> CameraShake;
    public void ShakeCam(float intensity, float time)
    {
        CameraShake?.Invoke(intensity, time);
    }
}
