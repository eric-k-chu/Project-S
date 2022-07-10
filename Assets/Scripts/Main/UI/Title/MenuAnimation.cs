using UnityEngine;
using DG.Tweening;

public class MenuAnimation : MonoBehaviour
{
    public float gridCycleLength;

    private void Start()
    {
        transform.DORotate(new Vector3(0, 360, 0), gridCycleLength, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear).SetRelative();
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
