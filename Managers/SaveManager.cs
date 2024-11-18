using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Linq;

namespace UIToolkitDemo
{
    // note: this uses JsonUtility for demo purposes only; for production work, consider a more performant solution like MessagePack (https://msgpack.org/index.html) 
    // or Protocol Buffers (https://developers.google.com/protocol-buffers)
    // 

    [RequireComponent(typeof(GameDataManager))]
    public class SaveManager : MonoBehaviour
    {

        public static event Action<GameData> GameDataLoaded;

        [Tooltip("Filename to save game and settings data")]
        [SerializeField] string m_SaveFilename = "savegame.dat";
        [Tooltip("Show Debug messages.")]
        [SerializeField] bool m_Debug;
        [SerializeField] bool m_IsLoadFormJson;

        GameDataManager m_GameDataManager;

        void Awake()
        {
            m_GameDataManager = GetComponent<GameDataManager>();
            LoadGame();
        }

        void OnApplicationQuit()
        {
            SaveGame();
        }

        void OnEnable()
        {
            SettingsEvents.SettingsShown += OnSettingsShown;
            SettingsEvents.SettingsUpdated += OnSettingsUpdated;

            GameplayEvents.SettingsUpdated += OnSettingsUpdated;

        }

        void OnDisable()
        {
            SettingsEvents.SettingsShown -= OnSettingsShown;
            SettingsEvents.SettingsUpdated -= OnSettingsUpdated;

            GameplayEvents.SettingsUpdated -= OnSettingsUpdated;

        }
        public GameData NewGame()
        {
            CharacterBaseSO[] allCharacters = Resources.LoadAll<CharacterBaseSO>("GameData/Characters");
            List<CharacterBaseSO> ownCharacters = new List<CharacterBaseSO>();
            for (int i = 0; i < Mathf.Min(3, allCharacters.Length); i++) // 确保不会超过实际数量
                ownCharacters.Add(allCharacters[i]);

            // 从 Resources/GameData/Equipment 加载装备
            List<EquipmentSO> ownEquipment = Resources.LoadAll<EquipmentSO>("GameData/Equipment")
                .Take(3) // 取前三个
                .ToList(); 

            // 从 Resources/GameData/Levels 加载关卡
            List<LevelSO> levelSOs = new List<LevelSO>(Resources.LoadAll<LevelSO>("GameData/Levels"));

            // 从 Resources/GameData/MailMessages 加载邮件消息
            List<MailMessageSO> mailMessageSOs = new List<MailMessageSO>(Resources.LoadAll<MailMessageSO>("GameData/MailMessages"));

            // 从 Resources/GameData/Chat 加载聊天数据
            List<ChatSO> chatSOs = new List<ChatSO>(Resources.LoadAll<ChatSO>("GameData/Chat"));

            // 从 Resources/GameData/ShopItems 加载商店物品
            List<ShopItemSO> shopItemSOs = new List<ShopItemSO>(Resources.LoadAll<ShopItemSO>("GameData/ShopItems"));

            // 使用加载的资源初始化 GameData
            return new GameData(ownEquipment, new List<EquipmentSO>(), new List<EquipmentSO>(), ownCharacters, levelSOs, mailMessageSOs, chatSOs, shopItemSOs);
        }

        public void LoadGame()
        {
            // load saved data from FileDataHandler
            if (m_GameDataManager.GameData == null || !m_IsLoadFormJson)
            {
                if (m_Debug)
                {
                    Debug.Log("GAME DATA MANAGER LoadGame: Initializing game data.");
                }
                m_GameDataManager.GameData = NewGame();
            }
            else if (FileManager.LoadFromFile(m_SaveFilename, out var jsonString) && m_IsLoadFormJson)
            {
                m_GameDataManager.GameData.LoadJson(jsonString);

                if (m_Debug)
                {
                    Debug.Log("SaveManager.LoadGame: " + m_SaveFilename + " json string: " + jsonString);
                }
            }

            // notify other game objects 
            if (m_GameDataManager.GameData != null)
            {
                GameDataLoaded?.Invoke(m_GameDataManager.GameData);
            }
        }

        public void SaveGame()
        {
            string jsonFile = m_GameDataManager.GameData.ToJson();

            // save to disk with FileDataHandler
            if (FileManager.WriteToFile(m_SaveFilename, jsonFile) && m_Debug)
            {
                Debug.Log("SaveManager.SaveGame: " + m_SaveFilename + " json string: " + jsonFile);
            }
        }

        // Load the saved GameData and display on the Settings Screen
        void OnSettingsShown()
        {
            if (m_GameDataManager.GameData != null)
            {
                GameDataLoaded?.Invoke(m_GameDataManager.GameData);
            }
        }

        // Update the GameDataManager data and save
        void OnSettingsUpdated(GameData gameData)
        {
            m_GameDataManager.GameData = gameData;
            SaveGame();
        }
    }
}
