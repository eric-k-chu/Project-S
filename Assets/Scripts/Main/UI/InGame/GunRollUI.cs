using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunRollUI : MonoBehaviour
{
    private Image gunRollImage;

    [SerializeField] private PlayerData player;

    private float gunRollCooldown;
    private float gunRollCooldownTimer;
    private bool gunRollUsed;

    private void Awake()
    {
        gunRollImage = GetComponent<Image>();
        gunRollCooldown = player.gunRollCooldown;
    }

    private void Start()
    {
        GameEventManager.instance.onGunRollCooldown += StartGunRollCooldown;

        gunRollCooldownTimer = 0f;
        gunRollUsed = false;
        gunRollCooldown = player.gunRollCooldown;
    }

    private void Update()
    {
        if (gunRollCooldownTimer <= 0) { gunRollUsed = false; }

        if (gunRollUsed)
        {
            gunRollCooldownTimer -= Time.deltaTime;
            gunRollImage.fillAmount = gunRollCooldownTimer / gunRollCooldown;
        }
    }

    private void StartGunRollCooldown()
    {
        gunRollUsed = true;
        gunRollCooldownTimer = gunRollCooldown;
    }

    private void OnDestroy()
    {
        GameEventManager.instance.onGunRollCooldown -= StartGunRollCooldown;
    }

}

