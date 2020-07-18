using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShopUpgradeSystem
{
    //Class to store Item Upgrade data
    [CreateAssetMenu(fileName = "ShopItemsData", menuName = "ShopSystem/ShopItemsData", order = 1)]
    public class ShopItemScriptable : ScriptableObject
    {
        public ShopItem[] shopItems;
    }

    [System.Serializable]
    public class ShopItem
    {
        public string itemName;                 //name if item
        public int[] levelUnlockCost;           //all the unlockable levels item has
    }
}
