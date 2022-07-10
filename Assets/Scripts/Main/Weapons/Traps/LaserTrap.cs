using UnityEngine;
using DG.Tweening;

public class LaserTrap : MonoBehaviour
{
    [Header("LaserBeam Obj")]
    public GameObject childObj;

    [Header("How long the laser is turned on")]
    public float duration;

    [Header("How long the laser is turned off")]
    public float offDuration;

    [Header("Is the Laser permanently on?")]
    public bool isPersistent;

    private bool isOn;
    private float onTimer;
    private float offTimer;

    private void Start()
    {
        childObj.SetActive(true);
        onTimer = duration;
        offTimer = offDuration;
        isOn = true;
    }

    private void Update()
    {
        if (!isPersistent)
        {
            if (isOn)
            {
                onTimer -= Time.deltaTime;
                if (onTimer <= 0)
                {
                    onTimer = duration;
                    childObj.SetActive(false);
                    isOn = false;
                }
            }
            else
            {
                offTimer -= Time.deltaTime;
                if (offTimer <= 0)
                {
                    offTimer = offDuration;
                    childObj.SetActive(true);
                    isOn = true;
                }
            }
        }
    }
}
