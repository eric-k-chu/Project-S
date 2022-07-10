using UnityEngine;

public class Sword : MonoBehaviour
{
    private Animator animator;
    private FallingEventHandler groundChecker;
    private AnimEventHandler animationChecker;
    private Rigidbody rb;
    private PlayerMovement player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        groundChecker = GetComponent<FallingEventHandler>();
        animationChecker = GetComponent<AnimEventHandler>();
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerMovement>();
    }
    
    private void Update()
    {
        if (!PauseUI.isPaused && !player.inCutscene)
        {
            if (Input.GetKeyDown(KeyCode.U) && groundChecker.isGrounded && !animationChecker.isAnimating)
            {
                animator.SetTrigger("Attack");
            }

            if (Input.GetKeyDown(KeyCode.I) && !groundChecker.isGrounded && !animationChecker.isAnimating && rb.velocity.y > 0.01)
            {
                animator.SetTrigger("AirAttack");
                animationChecker.shakeCam = true;
            }
            
            if (Input.GetKeyDown(KeyCode.O) && StyleUI.instance.getCurrentStyle() >= (1000f * 0.33f) && !groundChecker.isGrounded && !animationChecker.isAnimating && rb.velocity.y > 0.01)
            {
                animator.SetTrigger("AirSkill");
                GameEventManager.instance.DecreaseStyleMeter();
                animationChecker.shakeCam = true;
            }
        }
    }
}
