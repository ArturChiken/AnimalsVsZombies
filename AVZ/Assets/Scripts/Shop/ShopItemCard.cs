using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemCard : MonoBehaviour
{
    public ShopItemScriptableObject thisItem;

    [SerializeField] public Image itemIcon;
    [SerializeField] public TMP_Text itemName;
    [SerializeField] public TMP_Text itemCost;

    private void Awake()
    {
        itemIcon.sprite = thisItem.icon;
        itemName.text = thisItem.name;
        itemCost.text = $"{thisItem.cost}";
    }

    public void Buy()
    {

    }
}
