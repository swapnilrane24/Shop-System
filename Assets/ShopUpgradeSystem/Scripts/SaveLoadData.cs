using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShopUpgradeSystem 
{
    public class SaveLoadData : MonoBehaviour
    {
        [SerializeField] private ShopUI shopUI;
        private bool canSave = false;
        //Method to initialize the SaveLoad Script
        public void Initialize()
        {
            //ClearData();
            if (PlayerPrefs.GetInt("GameStartFirstTime") == 1)  //if PlayerPrefs of "GameStartFirstTime" value is 1, means we are playing the game again
            {
                LoadData();                                     //so we load the data
            }
            else                                                //if its not 1, means we are playing the game 1st time
            {
                SaveData();                                     //save the data 1st
                PlayerPrefs.SetInt("GameStartFirstTime", 1);    //save the PlayerPrefs
            }
            canSave = true;
        }

        //this is Unity method which is called when game is crashed or in background or quit
        private void OnApplicationPause(bool pause)
        {
#if !UNITY_EDITOR
            if(canSave)
            {
                SaveData();
            }
#endif
        }

        private void Update()
        {
#if UNITY_EDITOR
            //needed only in editor
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SaveData();
            }
#endif
        }

        /// <summary>
        /// Method used to save the data
        /// </summary>
        public void SaveData()
        {
            //convert the data to string
            string shopDataString = JsonUtility.ToJson(shopUI.shopData);
            Debug.Log("Save:" + shopDataString);

            try
            {
                //save the string as json 
                System.IO.File.WriteAllText(Application.persistentDataPath + "/ShopData.json", shopDataString);
                Debug.Log("Data Saved");

            }
            catch (System.Exception e)
            {
                //if we get any error debug it
                Debug.Log("Error Saving Data:" + e);
                throw;
            }
        }

        //Method used to load the data
        private void LoadData()
        {
            try
            {
                //get the text data from json and stro it in string
                string shopDataString = System.IO.File.ReadAllText(Application.persistentDataPath + "/ShopData.json");
                Debug.Log("Load:" + shopDataString);
                shopUI.shopData = new ShopData();
                shopUI.shopData = JsonUtility.FromJson<ShopData>(shopDataString); //create ShopData from json

                Debug.Log("Data Loaded");
            }
            catch (System.Exception e)
            {
                Debug.Log("Error Loading Data:" + e);
                throw;
            }

        }

        /// <summary>
        /// Method to clear all the save data
        /// </summary>
        public void ClearData()
        {
            Debug.Log("Data Cleared");
            PlayerPrefs.SetInt("GameStartFirstTime", 0);
        }

    }
}
