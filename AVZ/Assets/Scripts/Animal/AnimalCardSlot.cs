using UnityEngine;
using UnityEngine.UI;

public class AnimalCardSlot : MonoBehaviour
{
    public bool isUnlocked;
    private bool isUsed;

    public Image icon;
    public GameObject cardObject;
    public Transform canvas;
    private Gamemanager gameManager;

    private GameObject currentCardInstance;

    private void Start()
    {
        if (!isUnlocked)
        {
            icon.GetComponent<Image>().color = new Color(60 / 255f, 60 / 255f, 60 / 255f);
            gameObject.GetComponent<Image>().color = new Color(40 / 255f, 40 / 255f, 40 / 255f);
            return;
        }

        GetComponent<Button>().onClick.AddListener(AddPrefabToLayout);
        gameManager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();
    }
    public void AddPrefabToLayout()
    {
        if (isUnlocked && !isUsed && gameManager.cardAmount < 8)
        {
            currentCardInstance = Instantiate(cardObject, canvas);
            LayoutRebuilder.ForceRebuildLayoutImmediate(canvas as RectTransform);
            gameManager.cardAmount++;
            isUsed = true;
        }
        else if (isUnlocked && isUsed)
        {
            Destroy(currentCardInstance);
            gameManager.cardAmount--;
            isUsed = false;
        }
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
