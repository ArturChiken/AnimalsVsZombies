using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shovel : MonoBehaviour
{
    AudioManager audioManager;
    public Image icon;
    public Image image;
    public AudioClip soundToPlay;
    private Gamemanager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();
        GetComponent<Button>().onClick.AddListener(Use);
    }

    private void Update()
    {
        if (gameManager.canKill)
        {
            icon.color = new Color(0.24f, 0.24f, 0.24f);
            image.color = new Color(0.16f, 0.16f, 0.16f);
        }
        else if (!gameManager.canKill)
        {
            icon.color = new Color(1f, 1f, 1f);
            image.color = new Color(1f, 1f, 1f);
        }
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Use()
    {
        if (true && !gameManager.currentCrocodile && !gameManager.currentAnimal)
        {
            gameManager.UseShovel();
            audioManager.PlaySFX(soundToPlay);
        }
    }
}
