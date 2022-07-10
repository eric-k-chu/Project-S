using UnityEngine;
using DG.Tweening;

public class EnemyCoreController : MonoBehaviour
{
    public float rotateSpeed;
    public float moveSpeed;
    public Vector3 endPosition;

    public bool isMenu;

    private void Start()
    {
        transform.DORotate(new Vector3(360, 360, 360), rotateSpeed, RotateMode.FastBeyond360).SetLoops(-1).SetRelative().SetEase(Ease.Linear);

        if (!isMenu)
        {
            transform.DOMove(endPosition, moveSpeed).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            transform.DOMove(endPosition, moveSpeed).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        }
    }


    private void OnDestroy()
    {
        transform.DOKill();
    }
}
