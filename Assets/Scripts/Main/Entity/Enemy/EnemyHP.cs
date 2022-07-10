using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private EnemyData enemy;
    private float maximumHP;

    private Collider hurtbox;

    public bool isMob;

    private void Awake()
    {
        hurtbox = GetComponent<Collider>();
    }

    private void Start()
    {
        maximumHP = enemy.maximumHP;
        if (isMob)
        {
            hurtbox.enabled = true;
        }
        else
        {
            GameEventManager.instance.onBossEncounter += EnableHurtbox;
            hurtbox.enabled = false;
        }
    }

    private void Update()
    {
        if (maximumHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (GameEventManager.instance.hasBossSpawned)
        {
            GameEventManager.instance.EndBossEncounter();
        }
        
        if (isMob)
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            hurtbox.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (maximumHP > 0)
        {
            float damage = other.GetComponent<DamageInfo>().getDamage();
            maximumHP -= damage;
            GameEventManager.instance.EnemyTakeDamage(damage);
        } 
        else
        {
            Die();
        }
    }

    private void EnableHurtbox(Transform other)
    {
        hurtbox.enabled = true;
    }
}
