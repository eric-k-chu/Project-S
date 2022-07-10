using UnityEngine;

public class TitleUI : MonoBehaviour
{
    public GameObject CreditsMenu;

    public GameObject OptionsMenu;

    public void EnableCreditsMenu()
    {
        CreditsMenu.SetActive(true);
    }

    public void DisableCreditsMenu()
    {
        CreditsMenu.SetActive(false);
    }

    public void EnableOptionsMenu()
    {
        OptionsMenu.SetActive(true);
    }

    public void DisableOptionsMenu()
    {
        OptionsMenu.SetActive(false);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
