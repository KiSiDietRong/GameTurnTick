using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxSource;

    [Header("Sound Clips")]
    public AudioClip rotateTick;
    public AudioClip bellSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayRotateTick()
    {
        if (rotateTick != null && !sfxSource.isPlaying)
        {
            sfxSource.clip = rotateTick;
            sfxSource.loop = true;
            sfxSource.Play();
        }
    }

    public void StopRotateTick()
    {
        if (sfxSource.isPlaying)
        {
            sfxSource.Stop();
            sfxSource.clip = null;
        }
    }

    public void PlayBellSound()
    {
        if (bellSound != null)
            sfxSource.PlayOneShot(bellSound);
    }
}
