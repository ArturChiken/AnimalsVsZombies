using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemCard : MonoBehaviour
{
    public ShopItemScriptableObject thisItem;

    [SerializeField] public Image _itemIcon;
    [SerializeField] public TMP_Text _itemName;
    [SerializeField] public TMP_Text _itemCost;

    private void Awake()
    {
        _itemIcon.sprite = thisItem.icon;
        _itemName.text = thisItem.name;
        _itemCost.text = $"{thisItem.cost}";
    }
}
