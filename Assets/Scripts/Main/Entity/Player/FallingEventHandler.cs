using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingEventHandler : MonoBehaviour
{
    private Rigidbody rigidBody;
    private Animator animator;
    private AnimEventHandler animationChecker;

    private float fallMultiplier = 6f;

    // Ground Check Variables
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask groundMask;
    [HideInInspector] public bool isGrounded;
    private float groundCheckRadius = 0.1f;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        animationChecker = GetComponent<AnimEventHandler>();
    }

    private void FixedUpdate()
    {
        // Ground checks
        isGrounded = Physics.CheckSphere(groundCheckTransform.position, groundCheckRadius, groundMask, QueryTriggerInteraction.Ignore);
        animator.SetBool("isGrounded", isGrounded);

        // Fall Multiplier
        if (rigidBody.velocity.y < 0)
        {
            rigidBody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            if (!animationChecker.isAnimating)
            {
                animator.SetBool("isFalling", true);
            }
        }
        else
        {
            animator.SetBool("isFalling", false);
        }

        // Custom Gravity Scaling
        rigidBody.AddForce(Physics.gravity * rigidBody.mass * 2);
    }
}
