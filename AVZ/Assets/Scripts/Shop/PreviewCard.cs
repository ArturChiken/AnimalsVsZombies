using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PreviewCard : MonoBehaviour
{
    [SerializeField] public Image _itemIcon;
    [SerializeField] public TMP_Text _itemLabel;
    [SerializeField] public TMP_Text _itemCost;
    [SerializeField] public TMP_Text _itemDescription;

    public static PreviewCard _;
    private void Awake()
    {
        UpdateUI();
        if (_ == null)
            _ = this;
        else
            Debug.LogError("There are more than 1 PreviewCard in the scene");
    }

    public void UpdateUI()
    {
        _itemIcon.sprite = ShopManager._._activePreviewSO.icon;
        _itemLabel.text = ShopManager._._activePreviewSO.displayName;
        _itemCost.text = $"{ShopManager._._activePreviewSO.cost}";
        _itemDescription.text = ShopManager._._activePreviewSO.description;
    }
    public void BuyI()
    {
        ShopManager.BuyInfiniteItem(ShopManager._._activeShopItemSOInPreview);
    }

    public void BuyNI()
    {
        ShopManager.BuyCrocodilo(ShopManager._._activeShopItemSOInPreview);
    }

}
