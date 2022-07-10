using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;
using System.Collections.Generic;

public class AnimEventHandler : MonoBehaviour
{
    public enum Anim
    {
        NONE,
        IDLE,
        RUN,
        DASH,
        GUN,
        MELEE,
        JUMP,
        FALL,
        LAND,
        HURT,
        ROLL,
        AIR_ATTACK,
        AIR_SKILL,
    }


    [HideInInspector] public bool isAnimating = false;

    [SerializeField] private PlayerData player;

    private PlayerMovement movement;
    private Rigidbody rigidBody;
    private FallingEventHandler groundChecker;
    private Animator animator;
    private ParticleSystem explosionVFX;
    [HideInInspector] public bool shakeCam;

    public GameObject shockwaveRight;
    public GameObject shockwaveLeft;

    [Header("Gun Settings")]
    [SerializeField] private Transform muzzleLocation;
    [SerializeField] private GameObject bulletObj;

    [HideInInspector] public Anim prevAnim;

    private bool isMovingUp;
    private float moveUpTimer;


    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        rigidBody = GetComponent<Rigidbody>();
        groundChecker = GetComponent<FallingEventHandler>();
        animator = GetComponent<Animator>();
        explosionVFX = GetComponent<ParticleSystem>();
        
    }

    private void Update()
    {
        if (prevAnim == Anim.AIR_ATTACK || prevAnim == Anim.AIR_SKILL)
        {
            if (groundChecker.isGrounded && shakeCam)
            {
                CameraManager.instance.ShakeCam(3f, 0.3f);
                shakeCam = false;
                if (prevAnim == Anim.AIR_SKILL)
                {
                    shockwaveRight.SetActive(true);
                    shockwaveLeft.SetActive(true);
                }
                ResetAnimator();
                StartCoroutine(WaitForTime(0.3f));
            }
        }
    }

    private void FixedUpdate()
    {
        if (isMovingUp)
        {
            if (moveUpTimer > 0)
            {
                rigidBody.MovePosition(transform.position + (Vector3.up * 0.5f));
                moveUpTimer -= Time.deltaTime;
            }
            else
            {
                isMovingUp = false;
            }
        }
    }

    IEnumerator WaitForTime(float time)
    {
        yield return new WaitForSeconds(time);
        animator.SetTrigger("HitsGround");
    }

    private void DisableInputEvent(float flag)
    {
        if (flag == 0) 
        { 
            isAnimating = false; 
        }
        else if(flag == 1) 
        { 
            isAnimating = true; 
        } 
        else
        {
            return;
        }
    }

    private void AirMovesUpwardForce()
    {
        isMovingUp = true;
        moveUpTimer = 0.1f;
    }

    #region Movement methods

    private void zeroVelocity()
    {
        rigidBody.velocity = Vector3.zero;
    }

    private void zeroVelocityX()
    {
        rigidBody.velocity = new Vector3(0f, rigidBody.velocity.y);
    }

    private void DisableMoveEvent(float flag)
    {
        if (flag == 0)
        {
            movement.setCanMove(true);
        }
        else if (flag == 1)
        {
            if (!movement.isOnPlatform)
            {
                movement.setCanMove(false);
            }
        }
        else
        {
            return;
        }
    }

    private void MoveForwardEvent(float force)
    {
        if (movement.getDirection() == 1)
        {
            rigidBody.AddForce(Vector3.right * force, ForceMode.Impulse);
        }
        else if (movement.getDirection() == -1)
        {
            rigidBody.AddForce(Vector3.left * force, ForceMode.Impulse);
        }
        else
        {
            return;
        }
    }

    private void DisableGravityEvent(float flag)
    {
        if (flag == 0)
        {
            rigidBody.useGravity = true;
        }
        else
        {
            rigidBody.useGravity = false;
        }
    }

    private void MoveDownEvent(float force)
    {
        rigidBody.AddForce(Vector3.down * force, ForceMode.Impulse);
    }

    private void MoveBackwardEvent(float force)
    {
        if (movement.getDirection() == 1)
        {
            rigidBody.AddForce(Vector3.left * force, ForceMode.Impulse);
        }
        else if (movement.getDirection() == -1)
        {
            rigidBody.AddForce(Vector3.right * force, ForceMode.Impulse);
        }
        else
        {
            return;
        }
    }

    private void DashEvent()
    {
        if (movement.getXRaw() != 0)
        {
            if (movement.getDirection() == 1)
            {
                rigidBody.AddForce(Vector3.right * player.dashVelocity, ForceMode.Impulse); ;
            }
            else if (movement.getDirection() == -1)
            {
                rigidBody.AddForce(Vector3.left * player.dashVelocity, ForceMode.Impulse);
            }
        }
        else
        {
            if (movement.getDirection() == 1)
            {
                rigidBody.AddForce(Vector3.right * (player.dashVelocity + player.moveSpeed), ForceMode.Impulse);
            }
            else if (movement.getDirection() == -1)
            {
                rigidBody.AddForce(Vector3.left * (player.dashVelocity + player.moveSpeed), ForceMode.Impulse);
            }
        }

    }

    #endregion

    private void FireBulletEvent()
    {
        Instantiate(bulletObj, muzzleLocation);
    }

    private void PlayExplosionVFX()
    {
        explosionVFX.Play();
    }

    private void StopExplosionVFX()
    {
        explosionVFX.Stop();
    }

    private void SetAnim(float anim)
    {
        switch (anim)
        {
            case 0:
                prevAnim = Anim.IDLE;
                break;
            case 1:
                prevAnim = Anim.RUN;
                break;
            case 2:
                prevAnim = Anim.DASH;
                break;
            case 3:
                prevAnim = Anim.GUN;
                break;
            case 4:
                prevAnim = Anim.MELEE;
                break;
            case 5:
                prevAnim = Anim.JUMP;
                break;
            case 6:
                prevAnim = Anim.FALL;
                break;
            case 7:
                prevAnim = Anim.LAND;
                break;
            case 8:
                prevAnim = Anim.HURT;
                break;
            case 9:
                prevAnim = Anim.ROLL;
                break;
            case 10:
                animator.ResetTrigger("HitsGround");
                prevAnim = Anim.AIR_ATTACK;
                break;
            case 11:
                animator.ResetTrigger("HitsGround");
                prevAnim = Anim.AIR_SKILL;
                break;
            default:
                prevAnim = Anim.NONE;
                break;
        }
    }

    public void ResetAnimator ()
    {
        switch (prevAnim)
        {
            case Anim.IDLE:
                break;

            case Anim.RUN:
                break;

            case Anim.DASH:
                zeroVelocity();
                DisableMoveEvent(0);
                DisableInputEvent(0);
                break;

            case Anim.GUN:
                DisableMoveEvent(0);
                break;

            case Anim.MELEE:
                DisableMoveEvent(0);
                DisableInputEvent(0);
                break;

            case Anim.JUMP:
                break;

            case Anim.FALL:
                break;

            case Anim.LAND:
                zeroVelocity();
                break;

            case Anim.HURT:
                break;

            case Anim.ROLL:
                zeroVelocityX();
                DisableMoveEvent(0);
                DisableInputEvent(0);
                explosionVFX.Stop();
                break;

            case Anim.AIR_ATTACK:
                DisableMoveEvent(0);
                DisableInputEvent(0);
                break;
            case Anim.AIR_SKILL:
                DisableMoveEvent(0);
                DisableInputEvent(0);
                DisableGravityEvent(0);
                break;

            default:
                break;
        }
    }

    public void ResetTriggers ()
    {
        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Dash");
        animator.ResetTrigger("GunStationary");
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("GunRoll");
        animator.ResetTrigger("Hurt");
        animator.ResetTrigger("GunMove");
        animator.ResetTrigger("AirAttack");
        animator.ResetTrigger("AirSkill");
        animator.ResetTrigger("HitsGround");
    }

}

