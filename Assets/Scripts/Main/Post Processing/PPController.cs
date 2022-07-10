using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PPController : MonoBehaviour
{
    [HideInInspector] public Volume volume;
    private ChromaticAberration chromaticAberration;
    private LensDistortion lensDistortion;

    private bool inPortStoneCutScene;
    private bool inTeleportAnimation;
    [SerializeField] private AnimationCurve PortStoneTeleportationCurve;
    private int count = 0;

    private float CAMinIntensity = 0f;
    private float CAMaxIntensity = 1f;
    private float CAIntensityValue = 0.0f;

    private float LDTime = 0f;
    private float LDMaxTime = 1f;
    private float LDIntensityValue = 0.0f;

    private void Start()
    {
        GameEventManager.instance.onPortStoneEncounter += EnablePortStoneCutscene;
        volume.profile.TryGet<ChromaticAberration>(out chromaticAberration);
        volume.profile.TryGet<LensDistortion>(out lensDistortion);
        inPortStoneCutScene = false;
        inTeleportAnimation = false;
    }

    private void Update()
    {
        if (inPortStoneCutScene)
        {
            chromaticAberration.intensity.value = Mathf.Lerp(CAMinIntensity, CAMaxIntensity, CAIntensityValue);
            CAIntensityValue += Time.deltaTime;

            if (CAIntensityValue > 1.0f)
            {
                float temp = CAMaxIntensity;
                CAMaxIntensity = CAMinIntensity;
                CAMinIntensity = temp;
                CAIntensityValue = 0.0f;
                count++;

                if (count == 3)
                {
                    inPortStoneCutScene = false;
                    inTeleportAnimation = true;
                    count = 0;
                }
            }
        }

        if (inTeleportAnimation)
        {
            lensDistortion.intensity.value = LDIntensityValue;
            LDTime += Time.deltaTime;
            LDIntensityValue = PortStoneTeleportationCurve.Evaluate(LDTime);

            if (LDTime >= LDMaxTime)
            {
                inTeleportAnimation = false;
                GameEventManager.instance.EndPortStoneEncounter();
                StartCoroutine(Waiting(1f));
            }
               
        }
    }

    IEnumerator Waiting(float time)
    {
        yield return new WaitForSeconds(time);
        GameEventManager.instance.WinGame();
    }

    private void EnablePortStoneCutscene()
    {
        inPortStoneCutScene = true;
        CameraManager.instance.ShakeCam(1f, 5f);
    }

    private void OnDestroy()
    {
        GameEventManager.instance.onPortStoneEncounter -= EnablePortStoneCutscene;
    }

}
