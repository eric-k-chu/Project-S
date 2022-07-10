using UnityEngine;
using DG.Tweening;

public class PlatformController : MonoBehaviour
{
    public float speed;
    public float xValue;

    private Rigidbody rb;

    [Header("USE ONLY IF BACKGROUND OBJ")]
    public bool isBackgroundObj;
    public Vector3 endPos;
    private float backgroundSpeed;
    public GameObject storageUnit;

    private void Awake()
    {
        if (!isBackgroundObj)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    private void Start()
    {
        backgroundSpeed = Random.Range(20.0f, 30.0f);

        if (!isBackgroundObj)
        {
            rb.DOMoveX(xValue, speed).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetSpeedBased(true).SetUpdate(UpdateType.Fixed).SetAutoKill(false);
        }
        else
        {
            transform.DOMove(endPos, backgroundSpeed).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetSpeedBased(true).OnStepComplete(activateObj).SetAutoKill(false);
        }
    }

    public void activateObj ()
    {
        if (storageUnit.activeSelf)
        {
            storageUnit.SetActive(false);
        } 
        else
        {
            storageUnit.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        if (!isBackgroundObj)
        {
            rb.DOKill();
        }
        else
        {
            transform.DOKill();
        }
    }
}
