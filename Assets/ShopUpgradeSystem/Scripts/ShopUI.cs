using UnityEngine;
using UnityEngine.UI;

namespace ShopUpgradeSystem
{
    public class ShopUI : MonoBehaviour
    {
        public GameObject[] itemsObj;                       //list to all the 3D models of items
        public ShopSaveScriptable shopSave;                 //ref to ShopSaveScriptable asset
        public ShopItemScriptable shopItemList;             //ref to ShopItemScriptable asset
        public Text unlockBtnText, upgradeBtnText, levelText, itemNameText; //ref to important text components
        public Button unlockBtn, upgradeBtn, nextBtn, previousButton;   //ref to important Buttons

        private int currentIndex = 0;                       //index of current item showing in the shop 
        private int selectedIndex;                          //actual selected item index

        private void Start()
        {
            selectedIndex = PlayerPrefs.GetInt("SelectedItem", 0);  //get the selectedIndex from PlayerPrefs
            currentIndex = selectedIndex;                           //set the currentIndex
            levelText.text = "Level " + shopSave.shopSaveItems[currentIndex].unlockedLevel; //set levelText
            itemNameText.text = shopSave.shopSaveItems[currentIndex].itemName; //set item name
            unlockBtn.onClick.AddListener(() => UnlockSelectButton());      //add listner to button
            upgradeBtn.onClick.AddListener(() => UpgradeButton());          //add listner to button
            nextBtn.onClick.AddListener(() => NextButton());                //add listner to button
            previousButton.onClick.AddListener(() => PreviousButton());     //add listner to button

            if (currentIndex == 0) previousButton.interactable = false;     //dont interact previousButton if currentIndex is 0
            //dont interact previousButton if currentIndex is shopItemList.shopItems.Length - 1
            if (currentIndex == shopItemList.shopItems.Length - 1) nextBtn.interactable = false;

            itemsObj[currentIndex].SetActive(true);                         //activate the object at currentIndex
            UnlockButtonStatus();                                           
            UpgradeButtonStatus();
        }

        /// <summary>
        /// Method called on Next button click
        /// </summary>
        private void NextButton()
        {
            if (currentIndex < shopItemList.shopItems.Length - 1)
            {
                itemsObj[currentIndex].SetActive(false);
                currentIndex++;
                itemsObj[currentIndex].SetActive(true);
                itemNameText.text = shopSave.shopSaveItems[currentIndex].itemName;
                if (currentIndex == shopItemList.shopItems.Length - 1)
                {
                    nextBtn.interactable = false;
                }

                if (!previousButton.interactable)
                {
                    previousButton.interactable = true;
                }

                levelText.text = "Level " + shopSave.shopSaveItems[currentIndex].unlockedLevel;

                UnlockButtonStatus();
                UpgradeButtonStatus();
            }
        }

        /// <summary>
        /// Method called on Previous button click
        /// </summary>
        private void PreviousButton()
        {
            if (currentIndex > 0)
            {
                itemsObj[currentIndex].SetActive(false);
                currentIndex--;
                itemsObj[currentIndex].SetActive(true);
                itemNameText.text = shopSave.shopSaveItems[currentIndex].itemName;
                if (currentIndex == 0)
                {
                    previousButton.interactable = false;
                }

                if (!nextBtn.interactable)
                {
                    nextBtn.interactable = true;
                }

                levelText.text = "Level " + shopSave.shopSaveItems[currentIndex].unlockedLevel;

                UnlockButtonStatus();
                UpgradeButtonStatus();
            }
        }

        /// <summary>
        /// Method called on Unlock button click
        /// </summary>
        private void UnlockSelectButton()
        {
            unlockBtnText.text = "Selected";
            selectedIndex = currentIndex;
            PlayerPrefs.SetInt("SelectedItem", currentIndex);
            unlockBtn.interactable = false;

            if (!shopSave.shopSaveItems[currentIndex].isUnlocked)
            {
                //Reduce Coins
                shopSave.shopSaveItems[currentIndex].isUnlocked = true;
                UpgradeButtonStatus();
            }
        }

        /// <summary>
        /// Method called on Upgrade button click
        /// </summary>
        private void UpgradeButton()
        {
            //Reduce Coins

            shopSave.shopSaveItems[currentIndex].unlockedLevel++;

            if (shopSave.shopSaveItems[currentIndex].unlockedLevel < shopItemList.shopItems[currentIndex].levelUnlockCost.Length)
            {
                upgradeBtnText.text = "Upgrade to Lvl " + (shopSave.shopSaveItems[currentIndex].unlockedLevel + 1);
            }
            else
            {
                upgradeBtn.interactable = false;
                upgradeBtnText.text = "Max Lvl Reached";
            }

            levelText.text = "Level " + shopSave.shopSaveItems[currentIndex].unlockedLevel;
        }

        /// <summary>
        /// Method to set Unlock button interactable and text sttus
        /// </summary>
        private void UnlockButtonStatus()
        {
            //if current item is unlocked
            if (shopSave.shopSaveItems[currentIndex].isUnlocked)
            {
                //if selectedIndex is not equal to currentIndex set unlockBtn interactable false else make it true
                unlockBtn.interactable = selectedIndex != currentIndex;
                //set the text
                unlockBtnText.text = selectedIndex == currentIndex ? "Selected" : "Select";
            }
            else if (!shopSave.shopSaveItems[currentIndex].isUnlocked) //if current item is not unlocked
            {
                unlockBtn.interactable = true;  //set the unlockbtn interactable
                unlockBtnText.text = shopSave.shopSaveItems[currentIndex].unlockCost + ""; //set the text as cost of item
            }
        }

        /// <summary>
        /// Method to set Upgrade button interactable and text sttus
        /// </summary>
        private void UpgradeButtonStatus()
        {
            //if current item is unlocked
            if (shopSave.shopSaveItems[currentIndex].isUnlocked)
            {
                //if unlockLevel of current item is less than its max level
                if (shopSave.shopSaveItems[currentIndex].unlockedLevel < shopItemList.shopItems[currentIndex].levelUnlockCost.Length)
                {
                    upgradeBtn.interactable = true;                     //make upgradeBtn interactable true
                                                                        //set the next level as value of upgrade button text
                    upgradeBtnText.text = "Upgrade to Lvl " + (shopSave.shopSaveItems[currentIndex].unlockedLevel + 1);
                }
                else   //if unlockLevel of current item is equal to max level
                {
                    upgradeBtn.interactable = false;                    //make upgradeBtn interactable false
                    upgradeBtnText.text = "Max Lvl Reached";
                }
            }
            else if (!shopSave.shopSaveItems[currentIndex].isUnlocked)  //if current item is not unlocked
            {
                upgradeBtn.interactable = false;                        //make upgradeBtn interactable false
                upgradeBtnText.text = "Upgrade to Lvl " + (shopSave.shopSaveItems[currentIndex].unlockedLevel + 1);
            }
        }
    }
}