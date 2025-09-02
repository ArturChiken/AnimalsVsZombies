using UnityEngine;
using UnityEngine.UI;


public class AnimalCardSlot : MonoBehaviour
{
    [Header("References")]
    public string itemId; // ID предмета
    public Image icon;
    public GameObject cardObject;
    public Transform canvas;

    [Header("UI Elements")]
    //public Text usesText;
    public Button cardButton;

    private Gamemanager gameManager;
    private GameObject currentCardInstance;
    private bool isUsed;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(AddPrefabToLayout);
        gameManager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();
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

        cardButton.interactable = true;
        GetComponent<Button>().onClick.AddListener(AddPrefabToLayout);

    }
    public void AddPrefabToLayout()
    {

        if (!isUsed)
        {
            // Для предметов с ограниченным использованием

            currentCardInstance = Instantiate(cardObject, canvas);
            LayoutRebuilder.ForceRebuildLayoutImmediate(canvas as RectTransform);
            gameManager.cardAmount++;
            isUsed = true;
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

/*
// Настраиваем RectTransform (опционально)
RectTransform rectTransform = newItem.GetComponent<RectTransform>();
if (rectTransform != null)
{
    rectTransform.localScale = Vector3.one;
    rectTransform.anchoredPosition = Vector2.zero;
}
*/

// Обновляем layout (может потребоваться для немедленного обновления)
