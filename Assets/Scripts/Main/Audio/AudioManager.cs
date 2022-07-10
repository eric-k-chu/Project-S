using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip levelOne;
    public AudioClip boss;
    public AudioClip victory;

    private AudioSource source;

    private void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    public void PlayLevelOneTheme()
    {
        instance.source.clip = instance.levelOne;
        instance.source.PlayDelayed(0.5f);
    }

    public void PlayBossTheme()
    {
        instance.source.clip = instance.boss;
        instance.source.PlayDelayed(0.5f);
    }

    public void PlayVictoryClip()
    {
        instance.source.clip = instance.victory;
        instance.source.loop = false;
        instance.source.PlayDelayed(0.5f);
    }
}
