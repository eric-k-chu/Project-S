using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashUI : MonoBehaviour
{
    private Image dashImage;

    [SerializeField] private PlayerData player;

    private float dashCooldown;
    private float dashCooldownTimer;
    private bool dashUsed;


    private void Awake()
    {
        dashImage = GetComponent<Image>();
        dashCooldown = player.dashCooldown;
    }

    private void Start()
    {
        GameEventManager.instance.onDashCooldown += StartDashCooldown;
        dashCooldownTimer = 0f;
        dashUsed = false;
        dashCooldown = 3f;
    }

    private void Update()
    {
        if (dashCooldownTimer <= 0) { dashUsed = false; }

        if (dashUsed)
        {
            dashCooldownTimer -= Time.deltaTime;
            dashImage.fillAmount = dashCooldownTimer / dashCooldown;
        }
    }

    private void StartDashCooldown()
    {
        dashUsed = true;
        dashCooldownTimer = dashCooldown;
    }

    private void OnDestroy()
    {
        GameEventManager.instance.onDashCooldown -= StartDashCooldown;
    }
}
