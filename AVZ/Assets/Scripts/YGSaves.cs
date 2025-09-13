using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Analytics;

namespace YG
{
    public partial class SavesYG
    {
        // данные для сохранения
        public string unlockedShopItems = "";
        public string unlockedAchievements = "";
        public int unlockedLevels = 1;
        public int unlockedActs = 1;
        public int playerCoins = 5;
        public int crocodiloUses = 0;
        public int soundValue = 0;
        public int playerStars = 0;
        public float score = 0;
        public List<int> stars = new List<int>() { 0, 0 };
    }
}
