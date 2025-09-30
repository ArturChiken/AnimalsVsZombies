using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnimalSlot : MonoBehaviour
{
    public int price;

    AudioManager audioManager;

    public Sprite animalSprite;
    public GameObject animalObject;
    public Image icon;
    public TextMeshProUGUI priceText;
    private Gamemanager gameManager;
    public AudioClip soundToPlay;

    private void Start()
    {
        gameManager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();
        GetComponent<Button>().onClick.AddListener(BuyAnimal);
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void BuyAnimal()
    {
        if (gameManager.coffees >= price && !gameManager.currentCrocodile && !gameManager.currentAnimal)
        {
            gameManager.coffees -= price;
            gameManager.BuyAnimal(animalObject, animalSprite);
            audioManager.PlaySFX(soundToPlay);
        }
    }

    private void OnValidate()
    {
        if (animalSprite)
        {
            icon.enabled = true;
            icon.sprite = animalSprite;
            priceText.text = price.ToString();
        }
        else
        {
            icon.enabled = false;
        }
    }
}