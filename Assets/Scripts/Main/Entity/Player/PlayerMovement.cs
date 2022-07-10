using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables

    private Rigidbody rigidBody;
    private Animator animator;
    private AnimEventHandler animationChecker;
    private FallingEventHandler groundChecker;

    [SerializeField] private PlayerData player;
    private float dashTimestamp;
    private float xRaw;

    // Movement Booleans
    private bool canMove;
    private bool isRight;
    private bool isLeft;
    private bool isJumping;
    private bool isDashing;

    [HideInInspector] public bool isOnPlatform;

    private Rigidbody platformRB;

    [HideInInspector] public bool inCutscene;

    #endregion

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        animationChecker = GetComponent<AnimEventHandler>();
        groundChecker = GetComponent<FallingEventHandler>();
    }

    private void Start()
    {
        GameEventManager.instance.onPortStoneEncounter += EnableCutscene;
        GameEventManager.instance.onBossCutsceneStart += EnableCutscene;
        GameEventManager.instance.onBossCutsceneEnd += DisableCutscene;
        GameEventManager.instance.onBossDeathCutsceneEnd += DisableCutscene;

        isRight = true;
        isLeft = false;
        isJumping = false;
        canMove = true;
        dashTimestamp = Time.time;

        inCutscene = false;
    }

    private void Update()
    {
        Inputs();
        FaceDirection();
    }

    private void FixedUpdate()
    {
        if (canMove) { Move(); } 

        if (isJumping) { Jump(); }

        if (isDashing) { Dash(); }

        if (isOnPlatform)
        {
            rigidBody.velocity = rigidBody.velocity + platformRB.velocity;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            platformRB = collision.gameObject.GetComponent<Rigidbody>();
            isOnPlatform = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            platformRB = null;
            isOnPlatform = false;
        }
    }

    // Face towards positive x or negative x
    private void FaceDirection()
    {
        // xRaw determines directional movement
        if (xRaw > 0)
        {
            isRight = true;
            isLeft = false;

        }
        else if (xRaw < 0)
        {
            isRight = false;
            isLeft = true;
        } 

        // Rotate 3D Model towards directional movement
        if (isRight)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));
        }
        else if (isLeft)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
        }
    }

    // Horizontal Movement
    private void Move()
    {
        rigidBody.velocity = new Vector3(xRaw * player.moveSpeed, rigidBody.velocity.y);

        if (isRight)
        {
            animator.SetFloat("Speed", rigidBody.velocity.x / player.moveSpeed);
        }
        else 
        {
            animator.SetFloat("Speed", -1 * (rigidBody.velocity.x / player.moveSpeed));
        }
    }

    private void Inputs()
    {
        if (!PauseUI.isPaused && !inCutscene)
        {
            if (canMove)
            {
                xRaw = Input.GetAxisRaw("Horizontal");
            }

            if (Input.GetButtonDown("Jump") && !animationChecker.isAnimating && groundChecker.isGrounded)
            {
                isJumping = true;
            }

            if (Input.GetKeyDown(KeyCode.L) && dashTimestamp <= Time.time && !animationChecker.isAnimating && groundChecker.isGrounded)
            {
                isDashing = true;
            }
        }
    }

    private void Jump()
    {
        isJumping = false;
        animator.SetTrigger("Jump");
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0);
        rigidBody.AddForce(Vector3.up * player.jumpVelocity, ForceMode.Impulse);
    }


    private void Dash()
    {
        isDashing = false;
        isOnPlatform = false;
        animator.SetTrigger("Dash");
        dashTimestamp = Time.time + player.dashCooldown;
        GameEventManager.instance.StartDashCooldown();
    }


    #region Getters and Setters

    public void EnableCutscene()
    {
        inCutscene = true;
        xRaw = 0;
    }

    public void DisableCutscene()
    {
        inCutscene = false;
    }

    // Getters

    public int getDirection()
    {
        if (isRight) 
        { 
            return 1; 
        }
        else 
        { 
            return -1; 
        }
 
    }

    public float getXRaw()
    {
        return xRaw;
    }

    // Setters
    public void setCanMove(bool val)
    {
        canMove = val;
    }
    #endregion

    private void OnDestroy()
    {
        GameEventManager.instance.onPortStoneEncounter -= EnableCutscene;
        GameEventManager.instance.onBossCutsceneStart -= EnableCutscene;
        GameEventManager.instance.onBossCutsceneEnd -= DisableCutscene;
        GameEventManager.instance.onBossDeathCutsceneEnd -= DisableCutscene;
    }
}
