using UnityEngine;

public class OptionsUI : MonoBehaviour
{
    public void EnableOptionsMenu()
    {
        gameObject.SetActive(true);
        UIManager.inOptions = true;
    }

    public void DisableOptionsMenu()
    {
        gameObject.SetActive(false);
        UIManager.inOptions = false;
    }
}
