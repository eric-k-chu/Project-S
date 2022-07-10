using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnCollision : MonoBehaviour
{
    public GameObject explosion;

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Bullet"))
        {
            Vector3 p = transform.position;
            Quaternion r = transform.rotation;
            Instantiate(explosion, p, r);
            Destroy(transform.parent.gameObject);
        }
    }
}
