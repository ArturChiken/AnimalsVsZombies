using UnityEngine;

public class ShopCardButtonsManager : MonoBehaviour
{ 
    [SerializeField] PreviewScriptableObject _thisPreview;
    [SerializeField] ShopItemScriptableObject _thisShopItem;

    public void ButtonClicked()
    {
        ShopManager._.ChangePreviewSO(_thisPreview, _thisShopItem);
    }
}

