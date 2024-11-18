using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Collections.Generic;


namespace UIToolkitDemo
{
    // stores consumable data (resources)
    [System.Serializable]
    public class GameData
    {
        public uint gold = 500;
        public uint gems = 50;
        public uint healthPotions = 6;
        public uint levelUpPotions = 80;

        public uint levelnum;

        [SerializeField]
        public List<EquipmentSO> allEquipment;

        [SerializeField]
        public List<EquipmentSO> unequippedEquipment;

        [SerializeField]
        public List<EquipmentSO> equippedEquipment;

        [SerializeField]
        public List<CharacterBaseSO> ownCharacters;

        [SerializeField]
        public List<LevelSO> levelSOs;

        [SerializeField]
        public List<MailMessageSO> mailMessageSOs;

        [SerializeField]
        public List<ChatSO> chatSOs;

        [SerializeField]
        public List<ShopItemSO> shopItemSOs;

        public string username;
        public string theme;

        public float musicVolume;
        public float sfxVolume;

        // non-functional, used for saving SettingsScreen values
        public bool isSlideToggled;
        public bool isToggled;
        public string dropdownSelection;
        public int buttonSelection;


        // constructor, starting values
        public GameData(
            List<EquipmentSO> _allEquipment, 
            List<EquipmentSO> _equippedEquipment, 
            List<EquipmentSO> _unequippedEquipment,
            List<CharacterBaseSO> _ownCharacters, 
            List<LevelSO> _levelSOs, 
            List<MailMessageSO> _mailMessageSOs, 
            List<ChatSO> _chatSOs, 
            List<ShopItemSO> _shopItemSOs
        )
        {
            // player stats/data
            this.gold = 500;
            this.gems = 50;
            this.healthPotions = 6;
            this.levelUpPotions = 80;

            // settings
            this.musicVolume = 80f;
            this.sfxVolume = 80f;

            this.username = "GUEST_123456";
            this.dropdownSelection = "Item1";
            this.theme = "Default";
            this.buttonSelection = 2;

            this.isSlideToggled = false;
            this.isToggled = false;
            this.allEquipment = _allEquipment ?? new List<EquipmentSO>();
            this.equippedEquipment = _equippedEquipment ?? new List<EquipmentSO>();
            this.unequippedEquipment = _unequippedEquipment ?? new List<EquipmentSO>();
            this.ownCharacters = _ownCharacters ?? new List<CharacterBaseSO>();
            this.levelSOs = _levelSOs ?? new List<LevelSO>();
            this.mailMessageSOs = _mailMessageSOs ?? new List<MailMessageSO>();
            this.chatSOs = _chatSOs ?? new List<ChatSO>();
            this.shopItemSOs = _shopItemSOs ?? new List<ShopItemSO>();

        }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }

        public void LoadJson(string jsonFilepath)
        {
            JsonUtility.FromJsonOverwrite(jsonFilepath, this);
        }
    }
}