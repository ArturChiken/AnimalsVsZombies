using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrocoSlot : MonoBehaviour
{
    AudioManager audioManager;
    public AudioClip soundToPlay;

    public Sprite animalSprite;
    public GameObject crocoObject;
    public Image icon;
    public TextMeshProUGUI countText;
    private Gamemanager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();
        GetComponent<Button>().onClick.AddListener(BuyAnimal);
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        countText.text = gameManager.crocodileCount.ToString();
    }

    private void BuyAnimal()
    {
        if (true && !gameManager.currentCrocodile && !gameManager.currentAnimal && gameManager.crocodileCount > 0)
        {
            gameManager.crocodileCount--;
            gameManager.UseCroco(crocoObject);
            audioManager.PlaySFX(soundToPlay);
        }
    }

    private void OnValidate()
    {
        if (animalSprite)
        {
            icon.enabled = true;
            icon.sprite = animalSprite;
        }
        else
        {
            icon.enabled = false;
        }
    }
}
