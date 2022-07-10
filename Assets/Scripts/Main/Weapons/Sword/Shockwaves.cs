using UnityEngine;

public class Shockwaves : MonoBehaviour
{
    public bool isRight;
    public float speed;
    public float lifetime;
    private float time;
    private bool startTimer;

    public Transform player;
    private ParticleSystem explosionVFX;

    private void Awake()
    {
        explosionVFX = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        time = lifetime;
    }

    private void Update()
    {
        if (startTimer)
        {
            lifetime -= Time.deltaTime;
            if (lifetime <= 0)
            {
                explosionVFX.Stop();
                gameObject.SetActive(false);
            } 
            else
            {
                if (isRight)
                {
                    transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
                }
                else
                {
                    transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
                }
            }
        }
    }

    private void OnEnable()
    {
        explosionVFX.Play();
        if (isRight)
        {
            transform.position = player.position + new Vector3(2f, 1f, 0f);
        }
        else
        {
            transform.position = player.position + new Vector3(-2f, 1f, 0f);
        }
        startTimer = true;
    }

    private void OnDisable()
    {
        startTimer = false;
        lifetime = time;
    }
}
