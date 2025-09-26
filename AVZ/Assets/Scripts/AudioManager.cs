using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    //instance
    private static AudioManager _;

    [Header("AudioSource")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header ("AudioClip")]
    [Header("Music")]
    public AudioClip mainMenuMusic;
    public AudioClip inGameMusic;
    [Header("UI")]
    public AudioClip buttonClicked;
    public AudioClip buttonClicked2;
    public AudioClip buttonClicked3;
    public AudioClip woodtable;
    public AudioClip writing;
    public AudioClip purchase;
    public AudioClip leaderboardEntry;
    [Header("InGame")]
    public AudioClip win;
    public AudioClip endOfLvl;
    public AudioClip lose;
    public AudioClip coinCollect;
    public AudioClip coinDropped;
    [Header("Labubu")]
    public AudioClip labubu1;
    public AudioClip labubu2;
    public AudioClip labubu3;
    public AudioClip labubu4;
    [Header("Animals")]
    public AudioClip balerina;
    public AudioClip bobrito, bobrito_attack;
    public AudioClip bombordiro;
    public AudioClip cappuccinoA, cappuccinoA_attack;
    public AudioClip sahur, sahur_attack;
    public AudioClip shark, shark_attack;
    public AudioClip spioniro, spioniro_attack;
    public AudioClip camelo, camelo_attack;
    public AudioClip larila;

    void Awake()
    {
        if (_ == null)
        {
            _ = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void Start()
    {
        SwitchMusic(mainMenuMusic);
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void SwitchMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
