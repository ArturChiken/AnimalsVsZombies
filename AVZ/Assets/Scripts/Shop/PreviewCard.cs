using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PreviewCard : MonoBehaviour
{
    [SerializeField] public Image _itemIcon;
    [SerializeField] public TMP_Text _itemLabel;
    [SerializeField] public TMP_Text _itemCost;
    [SerializeField] public TMP_Text _itemDescription;

    private void Awake()
    {
        _itemIcon.sprite = ShopManager._._activePreviewSO.icon;
        _itemLabel.text = ShopManager._._activePreviewSO.name;
        _itemCost.text = $"{ShopManager._._activePreviewSO.cost}";
        _itemDescription.text = ShopManager._._activePreviewSO.description;
    }

    public void Buy()
    {
        ShopManager.BuyItem(ShopManager._._activeShopItemSOInPreview);
    }


    [ContextMenu("Is Bought")]
    public void DebugItsBought()
    {
        Debug.Log(ShopManager._._activeShopItemSOInPreview.isBought);
    }
}
