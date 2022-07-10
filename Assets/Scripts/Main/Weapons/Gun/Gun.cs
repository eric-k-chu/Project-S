using UnityEngine;

public class Gun : MonoBehaviour
{
    private Animator animator;
    private AnimEventHandler animationChecker;
    private FallingEventHandler groundChecker;
    private Rigidbody rb;
    private PlayerMovement player;

    [SerializeField] private PlayerData playerStats;

    private float gunRollTimestamp;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animationChecker = GetComponent<AnimEventHandler>();
        groundChecker = GetComponent<FallingEventHandler>();
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerMovement>();

    }

    private void Start()
    {
        gunRollTimestamp = Time.time;
    }

    private void Update()
    {
        if (!PauseUI.isPaused && !player.inCutscene)
        {
            // Firing Gun while stationary
            if (Input.GetKeyDown(KeyCode.J) && !animationChecker.isAnimating && groundChecker.isGrounded && Mathf.Abs(rb.velocity.x) < 0.01)
            {
                animator.SetTrigger("GunStationary");
            }
            // Firing Gun while moving
            if (Input.GetKeyDown(KeyCode.J) && !animationChecker.isAnimating && (Mathf.Abs(rb.velocity.x) > 0.01 || !groundChecker.isGrounded))
            {
                animator.SetTrigger("GunMove");
            }

            // Gun Roll
            if (Input.GetKeyDown(KeyCode.K) && !animationChecker.isAnimating && gunRollTimestamp <= Time.time)
            {
                gunRollTimestamp = Time.time + playerStats.gunRollCooldown;
                GameEventManager.instance.StartGunRollCooldown();
                animator.SetTrigger("GunRoll");
            }
        }
    }
}
