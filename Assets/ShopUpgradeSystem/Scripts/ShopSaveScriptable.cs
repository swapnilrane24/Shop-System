using UnityEngine;

namespace ShopUpgradeSystem
{
    //Class to save item unlock data
    [CreateAssetMenu(fileName = "ShopData", menuName = "ShopSystem/ShopData", order = 0)]
    public class ShopSaveScriptable : ScriptableObject
    {
        public ShopItem[] shopItems;
    }

    [System.Serializable]
    public class ShopItem
    {
        public string carName;             //name of item
        public bool isUnlocked;             //bool to check unlock status
        public int unlockCost;              //cost of unlock
        public int unlockedLevel = 1;       //level of item
        public CarLevelData[] carLevelsData;//array of all unlockable car levels
    }

    [System.Serializable]
    public class CarLevelData
    {
        public int unlockCost;
        public int speed;
        public int acceleration;
    }
}