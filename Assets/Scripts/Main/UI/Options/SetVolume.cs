using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer am;

    public void ChangeVolume(float val)
    {
        am.SetFloat("BGM_Vol", Mathf.Log10(val) * 20);
    }
}
