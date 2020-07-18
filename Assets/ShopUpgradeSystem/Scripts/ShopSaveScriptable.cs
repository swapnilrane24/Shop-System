using UnityEngine;

namespace ShopUpgradeSystem
{
    //Class to save item unlock data
    [CreateAssetMenu(fileName = "ShopSaveData", menuName = "ShopSystem/ShopSaveData", order = 0)]
    public class ShopSaveScriptable : ScriptableObject
    {
        public ShopSaveItem[] shopSaveItems;
    }

    [System.Serializable]
    public class ShopSaveItem
    {
        public string itemName;             //name of item
        public bool isUnlocked;             //bool to check unlock status
        public int unlockCost;              //cost of unlock
        public int unlockedLevel = 1;       //level of item
    }
}