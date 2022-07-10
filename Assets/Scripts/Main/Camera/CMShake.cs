using UnityEngine;
using Cinemachine;

public class CMShake : MonoBehaviour
{
    public CinemachineVirtualCamera cam;

    private CinemachineBasicMultiChannelPerlin perlin;
    private float shakeTime;
    private float shakeTimer;
    private float initialIntensity;

    private void Awake()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        perlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Start()
    {
        CameraManager.instance.CameraShake += CameraShake;
    }

    private void Update()
    {
        if (shakeTime > 0f)
        {
            shakeTime -= Time.deltaTime;
            if (shakeTime <= 0f)
            {
                perlin.m_AmplitudeGain = Mathf.Lerp(initialIntensity, 0f, 1 - (shakeTime / shakeTimer));          
            }
        }
    }

    public void CameraShake(float intensity, float time)
    {
        perlin.m_AmplitudeGain = intensity;
        initialIntensity = intensity;
        shakeTime = shakeTimer = time;
    }

    private void OnDestroy()
    {
        CameraManager.instance.CameraShake -= CameraShake;
    }
}
