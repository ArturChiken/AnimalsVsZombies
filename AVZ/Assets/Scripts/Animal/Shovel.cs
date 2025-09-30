using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shovel : MonoBehaviour
{
    AudioManager audioManager;
    public Transform trans;
    public AudioClip soundToPlay;
    private Gamemanager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();
        GetComponent<Button>().onClick.AddListener(Use);
    }

    private void Update()
    {
        if (gameManager.canKill) trans.localScale = new Vector3(2.3f, 2.3f, 2.3f);
        else if (!gameManager.canKill) trans.localScale = new Vector3(2f, 2f, 2f);
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
