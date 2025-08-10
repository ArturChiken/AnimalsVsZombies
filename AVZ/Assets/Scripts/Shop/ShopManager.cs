using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public void BuyItem(ShopItemScriptableObject item)
    {
        if (Gamemanager.currentCoinAmount < item.cost) 
        {
            Debug.LogError("Not enough coins");
            return;
        }
        item.isBought = true;
        Gamemanager.currentCoinAmount -= item.cost;

        PlayerPrefs.SetInt(item.name, 1);
        PlayerPrefs.SetInt($"{item.name}_count", item.useCount);
    }
}
