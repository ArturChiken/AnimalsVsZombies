using UnityEngine;
using System.Collections.Generic;

public static class SaveSystem
{
    private const string COINS_KEY = "PlayerCoins";
    private const string UNLOCKED_ITEMS_KEY = "UnlockedItems";
    private const string ITEM_USES_PREFIX = "ItemUses_";

    public static int CurrentCoinAmount
    {
        get => PlayerPrefs.GetInt(COINS_KEY, 0);
        set => PlayerPrefs.SetInt(COINS_KEY, value);
    }

    // сейв разблокированных предметов
    public static void UnlockItem(string itemId)
    {
        string unlockedItems = PlayerPrefs.GetString(UNLOCKED_ITEMS_KEY, "");
        if (!unlockedItems.Contains(itemId))
        {
            unlockedItems += unlockedItems.Length > 0 ? $",{itemId}" : itemId;
            PlayerPrefs.SetString(UNLOCKED_ITEMS_KEY, unlockedItems);
        }
    }

    // чек разблокирован ли предмет
    public static bool IsItemUnlocked(string itemId)
    {
        string unlockedItems = PlayerPrefs.GetString(UNLOCKED_ITEMS_KEY, "");
        return unlockedItems.Contains(itemId);
    }

    // сейв количества использований
    public static void SetItemUses(string itemId, int uses)
    {
        PlayerPrefs.SetInt($"{ITEM_USES_PREFIX}{itemId}", uses);
    }

    // получение количества использований
    public static int GetItemUses(string itemId)
    {
        return PlayerPrefs.GetInt($"{ITEM_USES_PREFIX}{itemId}", 0);
    }

    // декрис1 количества использований
    public static bool UseItem(string itemId)
    {
        int currentUses = GetItemUses(itemId);
        if (currentUses > 0)
        {
            SetItemUses(itemId, currentUses - 1);
            return true;
        }
        return false;
    }
}