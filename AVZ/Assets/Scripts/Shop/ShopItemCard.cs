using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ShopItemCard : MonoBehaviour
{
    public ShopItemScriptableObject thisItem;

    [SerializeField] public Image _itemIcon;
    [SerializeField] public TMP_Text _itemName;
    [SerializeField] public TMP_Text _itemCost;

    private void Awake()
    {
        _itemIcon.sprite = thisItem.icon;
        switch(YG2.lang)
        {
            case "ru":
                _itemName.text = thisItem.displayName;
                break;
            case "en":
                _itemName.text = thisItem.displayNameEn;
                break;
        }
        _itemCost.text = $"{thisItem.cost}";
    }
}
