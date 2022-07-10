using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 1f;

    private void Start()
    {
        transform.parent = null;
    }

    private void Update()
    {
        transform.Translate(0f, 0f, 20f * Time.deltaTime);

        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter()
    {
        Destroy(gameObject);
    }
}
