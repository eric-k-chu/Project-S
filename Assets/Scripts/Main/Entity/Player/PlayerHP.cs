using System.Collections;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private PlayerData player;
    [SerializeField] private Animator animator;
    [SerializeField] private AnimEventHandler animEvent;
    private SkinnedMeshRenderer playerMat;

    private float maximumHP;

    private bool blinkSwitch;
    private bool dmgTaken;
    private int onCount;
    private int offCount;

    private Collider hurtbox;

    public float invincibilityTime;

    private void Awake()
    {
        hurtbox = GetComponent<Collider>();
        playerMat = GetComponent<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        GameEventManager.instance.onPortStoneEncounterEnd += DisableMesh;
        hurtbox.enabled = true;
        maximumHP = player.maximumHP;
        blinkSwitch = true;
        dmgTaken = false;
        onCount = offCount = 0;
    }

    private void Update()
    {
        if (maximumHP <= 0)
        {
            Die();
        }
    }

    private void LateUpdate()
    {
        if (dmgTaken)
        {
            if (blinkSwitch)
            {
                playerMat.material.color = Color.white * 2f;
                onCount++;
                if (onCount >= 18) 
                { 
                    blinkSwitch = false;
                    onCount = 0;
                }
            }
            else
            {
                playerMat.material.color = Color.white * 1f;
                offCount++;
                if (offCount >= 18)
                {
                    blinkSwitch = true;
                    offCount = 0;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("InstantDeath"))
        {
            Die();
        }

        else if (maximumHP > 0)
        {
            hurtbox.enabled = false;

            float damage = other.GetComponent<DamageInfo>().getDamage();
            maximumHP -= damage;

            animator.SetTrigger("Hurt");
            animEvent.ResetAnimator();
            animEvent.prevAnim = AnimEventHandler.Anim.HURT;

            GameEventManager.instance.PlayerTakeDamage(damage);

            dmgTaken = true;

            StartCoroutine(InvincibilityTimer());
        }
        else
        {
            Die();
        }
    }


    IEnumerator InvincibilityTimer()
    {
        yield return new WaitForSeconds(invincibilityTime);
        playerMat.material.color = Color.white * 1f;
        hurtbox.enabled = true;
        dmgTaken = false;
    }

    private void Die()
    {
        DisableMesh();
        GameEventManager.instance.LoseGame();
    }

    private void DisableMesh()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameEventManager.instance.onPortStoneEncounterEnd -= DisableMesh;
    }
}
