using UnityEngine;

public class PreviewCard : MonoBehaviour
{
    public ShopItemScriptableObject thisItem;
    public void Buy()
    {
        ShopManager.BuyItem(thisItem);
    }

    [ContextMenu("Is Bought")]
    public void DebugItsBought()
    {
        Debug.Log(thisItem.isBought);
    }
}
