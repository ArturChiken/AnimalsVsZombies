using System.Collections.Generic;

namespace YG
{
    public partial class SavesYG
    {
        // данные для сохранения
        public string unlockedShopItems = "sahur";
        public string unlockedAchievements = "";

        public int unlockedLevels = 1;
        public int unlockedActs = 1;
        public int playerCoins = 5;
        public int crocodiloUses = 0;
        public int playerStars = 0;

        public int maxWaves = 0;
        public float musicVolume = 0.700f;
        public float SFXVolume = 0.700f;

        public List<int> stars = new List<int>() { 0, 0 };
        public List<string> consumableItems = new List<string>() { };

        public bool isFirstEntry = true;
    }
}
