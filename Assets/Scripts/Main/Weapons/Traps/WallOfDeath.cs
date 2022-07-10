using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WallOfDeath : MonoBehaviour
{
    public float moveSpeed;

    private Animator animator;
    private SphereCollider hitbox;
    private bool slowDown;
    private float animSpeed = 10;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        hitbox = GetComponentInChildren<SphereCollider>();
    }

    private void Start()
    {
        GameEventManager.instance.onCompactorEnter += StartTrashCompactor;
    }

    private void Update()
    {
        if (slowDown)
        {
            animator.SetFloat("Speed", animSpeed);
        }
    }

    private void OnDestroy()
    {
        GameEventManager.instance.onCompactorEnter -= StartTrashCompactor;
        transform.DOKill();
    }
    
    public void StartTrashCompactor()
    {
        transform.DORotate(new Vector3(0, 0, 52f), 1f).OnComplete(SpinSaw).SetEase(Ease.OutBounce);
    }

    private void SpinSaw()
    {
        CameraManager.instance.ShakeCam(5f, 0.3f);

        animator.SetBool("Spin", true);

        transform.DOMoveX(280f, moveSpeed).OnComplete(Stop).SetSpeedBased(true).SetEase(Ease.Linear);
    }

    public void Stop()
    {
        slowDown = true;
        DOTween.To(() => animSpeed, x => animSpeed = x, 0f, 1f).OnComplete(TurnOffCollider);
    }

    private void TurnOffCollider()
    {
        hitbox.enabled = false;
    }
}
