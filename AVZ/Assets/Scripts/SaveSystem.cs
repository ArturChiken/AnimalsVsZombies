using UnityEngine;
using YG;

public static class SaveSystem
{
    // Сохранение разблокированных предметов
    public static void UnlockItem(string itemId)
    {
        if (!YG2.saves.unlockedShopItems.Contains(itemId))
        {
            YG2.saves.unlockedShopItems += YG2.saves.unlockedShopItems.Length > 0 ? $",{itemId}" : itemId;
        }
    }

    public static bool IsItemUnlocked(string itemId)
    {
        return YG2.saves.unlockedShopItems.Contains(itemId);
    }

    public static void AddFiniteItem(string itemId)
    {
        YG2.saves.consumableItems.Add(itemId);
    }

}
