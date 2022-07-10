using System.Collections;
using UnityEngine;
using DG.Tweening;

public class BossController : MonoBehaviour
{
    public enum States
    {
        NONE,
        INTRO,
        IDLE,
        CHARGE,
        MOVE,
        ATTACK,
        WAIT,
        DEATH,
    }

    [Header("Waypoints")]
    [SerializeField] private Vector3 leftWayPoint;
    [SerializeField] private Vector3 rightWayPoint;

    [Header("Hitbox")]
    [SerializeField] private Collider hitbox;

    private Transform player;

    private Animator animator;
    private SkinnedMeshRenderer mesh;

    // State Variables
    private States currentState;

    // Timers
    [Header("Time in Intro State")]
    public float introTime;

    [Header("Time in Idle State")]
    public float idleTime;

    [Header("Time in Charge State")]
    public float chargeTime;

    [Header("Time After Slam Attack")]
    public float waitTime;

    [Header("Arena Shield Blockade")]
    public Transform shield;
    public GameObject shieldCollider;

    private float idleTimer;

    private float shieldScale = 0f;
    private float bossScale = 1f;

    // Booleans
    private bool isMoving;
    private bool triggerAttack;
    private bool isShieldEnabled;

    // Locations
    private Vector3 dest;
    private Vector3 playerPos;

    private void SetStateVariables()
    {
        currentState = States.NONE;
    }

    private void SetTimers()
    {
        idleTimer = idleTime;
    }

    private void SetBooleans()
    {
        isMoving = false;
        triggerAttack = false;
        hitbox.enabled = false;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        GameEventManager.instance.onBossEncounter += BossSpawn;
        GameEventManager.instance.onBossDeath += DieAnimation;
        mesh.enabled = false;

        animator.SetBool("INTRO", true);
        idleTimer = idleTime;
        SetStateVariables();
        SetBooleans();
    }

    private void Update()
    {
        if (isShieldEnabled)
        {
            shield.transform.localScale = Vector3.one * shieldScale;   
        }

        switch (currentState)
        {
            case States.INTRO:
                if (introTime == 0)
                {
                    animator.SetBool("INTRO", false);
                }
                break;

            case States.IDLE:
                HandleIdleState();
                break;
            case States.CHARGE:
                HandleChargeState();
                break;
            case States.MOVE:
                if (!isMoving)
                {
                    if (transform.position.x == rightWayPoint.x)
                    {
                        dest = leftWayPoint;

                    }
                    else if (transform.position.x == leftWayPoint.x)
                    {
                        dest = rightWayPoint;
                    }
                    transform.DOMove(dest, Random.Range(0.5f, 1f)).SetEase(Ease.OutQuart);
                    isMoving = true;
                }
                else
                {
                    HandleMoveState();
                }

                break;
            case States.ATTACK:
                HandleAttackState();
                break;
            case States.WAIT:
                HandleWaitState();
                break;
            case States.DEATH:
                transform.localScale = Vector3.one * bossScale;
                break;
            default:
                break;
        }
    }

    private void HandleIdleState()
    {
        Flip();
        if (idleTimer <= idleTime && idleTimer > 0f)
        {
            idleTimer -= Time.deltaTime;
            animator.SetBool("IDLE", true);
        }
        else
        {
            idleTimer = idleTime;
            animator.SetBool("IDLE", false);
            transitionState(States.CHARGE);
        }
    }

    private void HandleChargeState()
    {
        if (triggerAttack)
        {
            animator.SetTrigger("ATTACK CHARGE");
            StartCoroutine(Attack());
        }
        else
        {
            animator.SetTrigger("CHARGE");
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(chargeTime);
        triggerAttack = false;
        transitionState(States.ATTACK);
    }

    private void HandleMoveState()
    {
        if (transform.position.x != dest.x)
        {
            animator.SetBool("isMoving", isMoving);
            hitbox.enabled = true;
        }
        else
        {
            isMoving = false;
            animator.SetBool("isMoving", isMoving);
            hitbox.enabled = false;
            Flip();
            transitionState(States.IDLE);
            triggerAttack = true;
        }
    }

    private void HandleAttackState()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));
        hitbox.enabled = true;
        transform.position = playerPos;
        animator.SetTrigger("isAttacking");
    }
    
    private void HandleWaitState()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        hitbox.enabled = false;
        yield return new WaitForSeconds(waitTime);
        transform.position = dest;
        triggerAttack = false;
        animator.ResetTrigger("isAttacking");
        animator.ResetTrigger("ATTACK CHARGE");
        transitionState(States.IDLE);
    }

    // Transition to the next state
    private void transitionState(States state)
    {
        if (currentState == States.CHARGE)
        {
            animator.ResetTrigger("CHARGE");
        }
        currentState = state;
    }

    // Flip the model once it reaches a waypoint
    private void Flip()
    {
        if (dest == rightWayPoint)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }
        else if (dest == leftWayPoint)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
        }
    }

    private void BossSpawn(Transform playerTransform)
    {
        mesh.enabled = true;
        player = playerTransform;
        DOTween.To(() => introTime, x => introTime = x, 0f, introTime);
        transitionState(States.INTRO);
    }

    private void DestroyShield()
    {
        Destroy(shield.gameObject);
        Destroy(shieldCollider);
    }

    private void DieAnimation2()
    {
        // Scale Shield Arena to 0, Scale Boss to 0
        DOTween.To(() => shieldScale, x => shieldScale = x, 0f, 1f).SetEase(Ease.InOutExpo).OnComplete(DestroyShield);
        DOTween.To(() => bossScale, x => bossScale = x, 0f, 1f).SetEase(Ease.InOutExpo).OnComplete(Die);
        transitionState(States.DEATH);
    }

    private void Die()
    {
        GameEventManager.instance.EndBossDeathCutscene();
        Destroy(gameObject);
    }

    private void DieAnimation()
    {
        GameEventManager.instance.StartBossCutscene();
        AudioManager.instance.PlayLevelOneTheme();

        CameraManager.instance.ShakeCam(3f, 2);
        // Move Boss Up
        transform.DOMove(new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), 2f).OnComplete(DieAnimation2).SetEase(Ease.OutQuad);
        // Play Death Animation
        animator.SetTrigger("DEATH");
    }

    private void OnDestroy()
    {
        transform.DOKill();
        GameEventManager.instance.onBossEncounter -= BossSpawn;
        GameEventManager.instance.onBossDeath -= DieAnimation;
    }

    // Animation Event Methods

    private void GetPlayerPos()
    {
        playerPos = player.position;
    }

    private void CamShake()
    {
        CameraManager.instance.ShakeCam(7f, 3f);
    }

    private void EndCutscene()
    {
        GameEventManager.instance.EndBossCutscene();
    }

    private void EnableArenaShield()
    {
        isShieldEnabled = true;
        DOTween.To(() => shieldScale, x => shieldScale = x, 2.5f, 2f);
        shieldCollider.SetActive(true);
    }
}
