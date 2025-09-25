using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("AudioSource")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header ("AudioClip")]
    public AudioClip mainMenuMusic;
    public AudioClip inGameMusic;
    public AudioClip menuButtons;

    public AudioClip startSound;
    public AudioClip win;
    public AudioClip lose;
    public AudioClip coinCollect;

    public AudioClip labubu1;
    public AudioClip animal1;

    public AudioClip purchase;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        musicSource.clip = mainMenuMusic;
        musicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
