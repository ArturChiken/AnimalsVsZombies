using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YG;

public class CrocoSlot : MonoBehaviour
{
    AudioManager audioManager;

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
            YG2.saves.consumableItems.Remove("crocodilo");
            gameManager.UseCroco(crocoObject);
            audioManager.PlaySFX(audioManager.bombordiro);
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
