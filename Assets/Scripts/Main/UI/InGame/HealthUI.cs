using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{

    [SerializeField] private PlayerData player;
    // Image to adjust
    private Image healthBar;

    // Health Components;
    private float maximumHP;
    private float currentHP;

    private void Awake()
    {
        healthBar = GetComponent<Image>();
    }

    private void Start()
    {
        GameEventManager.instance.onPlayerDamageTaken += DecreaseHP;
        maximumHP = currentHP = player.maximumHP;
        healthBar.fillAmount = currentHP / maximumHP;
    }

    private void DecreaseHP(float dmg)
    {
        currentHP -= dmg;
        healthBar.fillAmount = currentHP / maximumHP;
    }

    private void OnDestroy()
    {
        GameEventManager.instance.onPlayerDamageTaken -= DecreaseHP;
    }
}
