using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class PreviewCard : MonoBehaviour
{
    AudioManager audioManager;
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
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private bool IsValid()
    {
        if (this == null) return false;
        if (_itemIcon == null) return false;
        if (_itemLabel == null) return false;
        if (_itemCost == null) return false;
        if (_itemDescription == null) return false;

        return true;
    }

    public void UpdateUI()
    {
        if (!IsValid()) return;

        _itemIcon.sprite = ShopManager._._activePreviewSO.icon;
        switch(YG2.lang)
        {
            case "ru":
                _itemLabel.text = ShopManager._._activePreviewSO.displayName;
                _itemDescription.text = ShopManager._._activePreviewSO.description;
                break;
            case "en":
                _itemLabel.text = ShopManager._._activePreviewSO.displayNameEn;
                _itemDescription.text = ShopManager._._activePreviewSO.descriptionEn;
                break;
        }
        _itemCost.text = $"{ShopManager._._activePreviewSO.cost}";
    }
    public void BuyI()
    {
        bool isItBuyed;
        ShopManager.BuyInfiniteItem(ShopManager._._activeShopItemSOInPreview, out isItBuyed);
        if (isItBuyed)
        {
            audioManager.PlaySFX(audioManager.purchase);
        }
        else
        {
            audioManager.PlaySFX(audioManager.buttonClicked);
        }
    }

    public void BuyNI()
    {
        ShopManager.BuyCrocodilo(ShopManager._._activeShopItemSOInPreview);
    }

}
