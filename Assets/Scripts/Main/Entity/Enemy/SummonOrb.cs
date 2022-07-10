using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonOrb : MonoBehaviour
{
    public GameObject proj;
    private GameObject target;
    public float triggerRange = 10f;
    public float delay = 1.5f;
    private bool canShoot = true;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.LookAt(target.transform.position);
        if (inRange() && canShoot == true)
        {
            StartCoroutine(DelayShot());
        }
    }

    bool inRange()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < triggerRange)
        {
            return true;
        }
        else
            return false;
    }

    IEnumerator DelayShot()
    {
        GameObject ball = Instantiate(proj, transform.position, transform.rotation);
        canShoot = false;
        yield return new WaitForSeconds(delay);
        canShoot = true;
    }
}
