using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimalCardSlot : MonoBehaviour
{
    public int price;
    public bool isUnlocked;

    public GameObject cardObject;
    public Transform canvas;

    private void Start()
    {
        if (!isUnlocked)
        {
            //GetComponent<SpriteRenderer>().color = Color.black;
            return;
        }

        GetComponent<Button>().onClick.AddListener(AddPrefabToLayout);
    }
    public void AddPrefabToLayout()
    {
        if (!isUnlocked)
        {
            return;
        }

        // ������� ��������� �������
        GameObject newItem = Instantiate(cardObject, canvas);
        /*
        // ����������� RectTransform (�����������)
        RectTransform rectTransform = newItem.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.localScale = Vector3.one;
            rectTransform.anchoredPosition = Vector2.zero;
        }
        */

        // ��������� layout (����� ������������� ��� ������������ ����������)
        LayoutRebuilder.ForceRebuildLayoutImmediate(canvas as RectTransform);
    }

}
