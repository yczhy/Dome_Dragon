using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;

namespace UIToolkitDemo
{
    /// <summary>
    /// This is a non-modal overlay screen, not managed by the UIManager. This can appear to notify the user with temporary
    /// text notifications.
    /// </summary>
    public class PopUpText : MenuScreen
    {
        // Rich text color highlight
        public static string TextHighlight = "F8BB19";

        // Contains basic text styling
        const string k_PopUpTextClass = "popup-text";

        // Each message contains its own styles

        // ShopScreen message classes
        const string k_ShopActiveClass = "popup-text-active";
        const string k_ShopInactiveClass = "popup-text-inactive";

        // CharScreen message classes
        const string k_GearActiveClass = "popup-text-active--left";
        const string k_GearInactiveClass = "popup-text-inactive--left";

        // HomeScreen message classes
        const string k_HomeActiveClass = "popup-text-active--home";
        const string k_HomeInactiveClass = "popup-text-inactive--home";

        // Mail message reward classes
        const string k_MailActiveClass = "popup-text-active--reward";
        const string k_MailInactiveClass = "popup-text-inactive--reward";

        // Delay between welcome messages
        const float k_HomeMessageCooldown = 15f;

        float m_TimeToNextHomeMessage = 0f;
        Label m_PopUpText;

        // Customizes active/inactive styles, duration, and delay for each text prompt
        float m_Delay = 0f;
        float m_Duration = 1f;
        string m_ActiveClass;
        string m_InactiveClass;
        
        // Check for portrait/landscape changes
        MediaQuery m_MediaQuery;
        MediaAspectRatio m_MediaAspectRatio;
        

        void OnEnable()
        {
            InventoryEvents.GearSelected += OnGearSelected;

            ShopEvents.TransactionProcessed += OnShopTransactionProcessed;
            ShopEvents.RewardProcessed += OnMailRewardProcessed;
            ShopEvents.TransactionFailed += OnTransactionFailed;

            CharEvents.CharacterLeveledUp += OnCharacterLeveledUp;

            HomeEvents.HomeMessageShown += OnHomeMessageShown;
        }

        void OnDisable()
        {
            InventoryEvents.GearSelected -= OnGearSelected;

            ShopEvents.TransactionProcessed -= OnShopTransactionProcessed;
            ShopEvents.RewardProcessed -= OnMailRewardProcessed;
            ShopEvents.TransactionFailed -= OnTransactionFailed;

            CharEvents.CharacterLeveledUp -= OnCharacterLeveledUp;

            HomeEvents.HomeMessageShown -= OnHomeMessageShown;
        }

        protected override void Awake()
        {
            base.Awake();
            SetVisualElements();

            m_ActiveClass = k_ShopActiveClass;
            m_InactiveClass = k_ShopInactiveClass;

            SetupText();
            HideText();
        }

        protected override void SetVisualElements()
        {
            base.SetVisualElements();
            m_PopUpText = m_Root.Q<Label>("main-menu-popup_text");

            if (m_PopUpText != null)
            {
                m_PopUpText.text = string.Empty;
            }
        }

        void ShowMessage(string message)
        {
            if (m_PopUpText == null || string.IsNullOrEmpty(message))
            {
                return;
            }

            StartCoroutine(ShowMessageRoutine(message));
        }

        IEnumerator ShowMessageRoutine(string message)
        {
            if (m_PopUpText != null)
            {
                m_PopUpText.text = message;

                // reset any leftover Selectors
                SetupText();

                // hide text
                HideText();

                // wait for delay
                yield return new WaitForSeconds(m_Delay);

                // show text and wait for duration
                ShowText();
                yield return new WaitForSeconds(m_Duration);

                // hide text again
                HideText();
            }
        }

        void HideText()
        {
            m_PopUpText.RemoveFromClassList(m_ActiveClass);
            m_PopUpText.AddToClassList(m_InactiveClass);
        }

        void ShowText()
        {
            m_PopUpText.RemoveFromClassList(m_InactiveClass);
            m_PopUpText.AddToClassList(m_ActiveClass);
        }

        // clear any remaining Selectors and add base styling 
        void SetupText()
        {
            m_PopUpText.ClearClassList();
            m_PopUpText.AddToClassList(k_PopUpTextClass);
        }

        // event-handling methods

        void OnTransactionFailed(ShopItemSO shopItemSO)
        {
            // use a longer delay, messages are longer here
            m_Delay = 0f;
            m_Duration = 1.2f;

            // centered on ShopScreen
            m_ActiveClass = k_ShopActiveClass;
            m_InactiveClass = k_ShopInactiveClass;

            string failMessage = "Insufficient funds for <color=#" + PopUpText.TextHighlight + ">" + shopItemSO.itemName + "</color>.\n  " +
                "Buy more <color=#" + PopUpText.TextHighlight + ">" + shopItemSO.CostInCurrencyType + "</color>.";
            ShowMessage(failMessage);
        }

        void OnGearSelected(EquipmentSO gear)
        {
            m_Delay = 0f;
            m_Duration = 0.8f;

            // centered on CharScreen
            m_ActiveClass = k_GearActiveClass;
            m_InactiveClass = k_GearInactiveClass;

            string equipMessage = "<color=#" + PopUpText.TextHighlight + ">" + gear.equipmentName + "</color> equipped.";
            ShowMessage(equipMessage);
        }

        void OnCharacterLeveledUp(bool state)
        {
            // only show text warning on failure
            if (!state)
            {
                // timing and position
                m_Delay = 0f;
                m_Duration = 0.8f;

                // centered on CharScreen
                m_ActiveClass = k_GearActiveClass;
                m_InactiveClass = k_GearInactiveClass;

                if (m_PopUpText != null)
                {
                    string equipMessage = "Insufficient potions to level up.";
                    ShowMessage(equipMessage);
                }
            }
        }

        void OnHomeMessageShown(string message)
        {
            // welcome the player when on the HomeScreen
            if (Time.time >= m_TimeToNextHomeMessage)
            {
                // timing and position
                m_Delay = 0.25f;
                m_Duration = 1.5f;

                // centered below title
                m_ActiveClass = k_HomeActiveClass;
                m_InactiveClass = k_HomeInactiveClass;

                ShowMessage(message);

                // cooldown delay to avoid spamming
                m_TimeToNextHomeMessage = Time.time + k_HomeMessageCooldown;
            }
        }

        void OnShopTransactionProcessed(ShopItemSO shopItem, Vector2 screenPos)
        {
            // show message when purchasing potions (not gold or gems)
            if (shopItem.contentType == ShopItemType.LevelUpPotion || shopItem.contentType == ShopItemType.HealthPotion)
            {

                // timing and position
                m_Delay = 0f;
                m_Duration = 0.8f;

                // centered on ShopScreen
                m_ActiveClass = k_ShopActiveClass;
                m_InactiveClass = k_ShopInactiveClass;

                string buyMessage = "<color=#" + PopUpText.TextHighlight + ">" + shopItem.itemName + "</color> added to inventory.";

                ShowMessage(buyMessage);
            }
        }

        void OnMailRewardProcessed(ShopItemType rewardType, uint rewardQuantity, Vector2 screenPos)
        {
            // show message when purchasing potions (not gold or gems)
            if (rewardType == ShopItemType.LevelUpPotion || rewardType == ShopItemType.HealthPotion)
            {

                // timing and position
                m_Delay = 0f;
                m_Duration = 1.2f;
                
                // centered on right Mail Content Screen
                m_ActiveClass = k_MailActiveClass;
                m_InactiveClass = k_MailInactiveClass;

                string plural = (rewardQuantity > 1) ? "s" : string.Empty;
                string buyMessage = "<color=#" + PopUpText.TextHighlight + ">" + rewardQuantity + " " + rewardType + plural + "</color> added to inventory.";

                ShowMessage(buyMessage);
            }
        }
    }
}
