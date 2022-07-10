using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UILoader : MonoBehaviour
{
    public void SwitchScene(string sceneName)
    {
        DOTween.Clear(true);
        if (!PauseUI.toMenu)
        {
            PauseUI.toMenu = true;
            SceneManager.LoadScene(sceneName);
        } 
        else
        {
            SceneManager.LoadScene(sceneName);
        }
        
    }
}
