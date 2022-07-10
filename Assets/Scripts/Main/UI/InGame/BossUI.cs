using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    [SerializeField] private EnemyData enemy;

    private Image healthBar;

    private float maximumHP;
    private float currentHP;

    private void Awake()
    {
        healthBar = GetComponent<Image>();    
    }

    private void Start()
    {
        GameEventManager.instance.onEnemyDamageTaken += DecreaseHP;
        maximumHP = currentHP = enemy.maximumHP;
        healthBar.fillAmount = currentHP / maximumHP;
    }

    private void DecreaseHP(float dmg)
    {
        currentHP -= dmg;
        healthBar.fillAmount = currentHP / maximumHP;
    }

    private void OnDestroy()
    {
        GameEventManager.instance.onEnemyDamageTaken -= DecreaseHP;
    }
}
