using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnimalSlot : MonoBehaviour
{
    public int price;

    public Sprite animalSprite;
    public GameObject animalObject;
    public Image icon;
    public TextMeshProUGUI priceText;
    private Gamemanager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();
        GetComponent<Button>().onClick.AddListener(BuyAnimal);
    }

    private void BuyAnimal()
    {
        if (gameManager.coffees >= price && !gameManager.currentAnimal)
        {
            gameManager.coffees -= price;
            gameManager.BuyAnimal(animalObject, animalSprite);
        }

        gameManager.BuyAnimal(animalObject, animalSprite);
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
