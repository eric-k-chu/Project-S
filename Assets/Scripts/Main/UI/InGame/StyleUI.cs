using UnityEngine;
using UnityEngine.UI;

public class StyleUI : MonoBehaviour
{

    public static StyleUI instance;

    [SerializeField] private PlayerData player;
    // Image to adjust
    private Image styleBar;

    // Style Components
    private float maximumStyle;
    private float currentStyle;

    private void Awake()
    {
        instance = this;
        styleBar = GetComponent<Image>();
    }

    private void Start()
    {
        GameEventManager.instance.onEnemyDamageTaken += IncreaseStyle;
        GameEventManager.instance.onAirSkillUse += DecreaseStyle;
        maximumStyle = player.maximumStyle;
        currentStyle = 330f;
        styleBar.fillAmount = currentStyle / maximumStyle;
    }

    private void IncreaseStyle(float dmg)
    {
        currentStyle += (dmg / 25f);
        styleBar.fillAmount = currentStyle / maximumStyle;
    }

    private void DecreaseStyle()
    {
        currentStyle -= 330f;
        if (currentStyle < 0f)
        {
            currentStyle = 0f;
        }
        styleBar.fillAmount = currentStyle / maximumStyle;
    }

    private void OnDestroy()
    {
        GameEventManager.instance.onEnemyDamageTaken -= IncreaseStyle;
    }

    public float getCurrentStyle()
    {
        return currentStyle;
    }
}
