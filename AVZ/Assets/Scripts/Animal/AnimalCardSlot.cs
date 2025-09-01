using UnityEngine;
using UnityEngine.UI;

public class AnimalCardSlot : MonoBehaviour
{
    [Header("References")]
    public string itemId; // ID айтема
    public Image icon;
    public GameObject cardObject;
    public Transform canvas;

    [Header("UI Elements")]
    public Text usesText;
    public Button cardButton;

    private Gamemanager gameManager;
    private GameObject currentCardInstance;
    private bool isUsed;

    private void Start()
    {
        gameManager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();
        InitializeCard();
    }

    private void InitializeCard()
    {
        bool isUnlocked = SaveSystem.IsItemUnlocked(itemId);

        if (!isUnlocked)
        {
            icon.color = new Color(0.24f, 0.24f, 0.24f);
            GetComponent<Image>().color = new Color(0.16f, 0.16f, 0.16f);
            cardButton.interactable = false;
            return;
        }

        // для разблокированных карточек
        cardButton.interactable = true;
        GetComponent<Button>().onClick.AddListener(AddPrefabToLayout);

        // обновляем отображение количества использований
        UpdateUsesDisplay();
    }

    private void UpdateUsesDisplay()
    {
        int uses = SaveSystem.GetItemUses(itemId);

        if (uses == -1) // перманентный предмет
        {
          
        }
        else if (uses > 0) // айтем с ограниченным использованием
        {
            usesText.text = uses.ToString();
            usesText.gameObject.SetActive(true);
        }
        else // закончились использования
        {
            usesText.gameObject.SetActive(false);
            cardButton.interactable = false;
            icon.color = new Color(0.5f, 0.5f, 0.5f);
        }
    }

    public void AddPrefabToLayout()
    {
        int uses = SaveSystem.GetItemUses(itemId);

        // проверка можно ли использовать предмет
        if (uses == 0) return;
        if (gameManager.cardAmount >= 8) return;

        if (!isUsed)
        {
            // с ограниченным использованием
            if (uses > 0)
            {
                if (!SaveSystem.UseItem(itemId))
                    return;
            }

            currentCardInstance = Instantiate(cardObject, canvas);
            LayoutRebuilder.ForceRebuildLayoutImmediate(canvas as RectTransform);
            gameManager.cardAmount++;
            isUsed = true;

            UpdateUsesDisplay(); // обнова UI после использования
        }
        else
        {
            Destroy(currentCardInstance);
            gameManager.cardAmount--;
            isUsed = false;
        }
    }

    public void RefreshCard()
    {
        InitializeCard();
    }
}