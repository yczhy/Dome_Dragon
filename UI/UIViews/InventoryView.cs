using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIToolkitDemo
{
    public class InventoryView : UIView
    {

        ScrollView m_ScrollViewParent;

        VisualElement m_InventoryBackButton;
        VisualElement m_InventoryPanel;

        DropdownField m_InventoryRarityDropdown;
        DropdownField m_InventorySlotTypeDropdown;

        VisualTreeAsset m_GearItemAsset;

        // Actively checked gear
        GearItemComponent m_SelectedGear;

        public InventoryView(VisualElement topElement): base(topElement)
        {
            InventoryEvents.GearItemClicked += OnGearItemClicked;
            InventoryEvents.InventorySetup += OnInventorySetup;
            InventoryEvents.InventoryUpdated += OnInventoryUpdated;

            m_GearItemAsset = Resources.Load("GearItem") as VisualTreeAsset;
        }

        public override void Dispose()
        {
            base.Dispose();
            InventoryEvents.GearItemClicked -= OnGearItemClicked;
            InventoryEvents.InventorySetup -= OnInventorySetup;
            InventoryEvents.InventoryUpdated -= OnInventoryUpdated;

            UnregisterButtonCallbacks();
        }

        protected override void SetVisualElements()
        {
            base.SetVisualElements();

            m_InventoryBackButton = m_TopElement.Q("inventory__back-button");
            m_InventoryPanel = m_TopElement.Q("inventory__screen");
            m_InventoryRarityDropdown = m_TopElement.Q<DropdownField>("inventory__rarity-dropdown");
            m_InventorySlotTypeDropdown = m_TopElement.Q<DropdownField>("inventory__slot-type-dropdown");

            // define row elements under the scrollview
            m_ScrollViewParent = m_TopElement.Q<ScrollView>("inventory__scrollview");
        }

        protected override void RegisterButtonCallbacks()
        {
            m_InventoryBackButton.RegisterCallback<ClickEvent>(CloseWindow);

            // register callbacks when value in a dropdown field changes
            m_InventoryRarityDropdown.RegisterValueChangedCallback(UpdateFilters);
            m_InventorySlotTypeDropdown.RegisterValueChangedCallback(UpdateFilters);
        }

        // Optional: Unregistering the button callbacks is not strictly necessary
        // in most cases and depends on your application's lifecycle management.
        // You can choose to unregister them if needed for specific scenarios.
        protected void UnregisterButtonCallbacks()
        {
            m_InventoryBackButton.UnregisterCallback<ClickEvent>(CloseWindow);

            // register callbacks when value in a dropdown field changes
            m_InventoryRarityDropdown.UnregisterValueChangedCallback(UpdateFilters);
            m_InventorySlotTypeDropdown.UnregisterValueChangedCallback(UpdateFilters);
        }

        // convert string to Rarity enum
        Rarity GetRarity(string rarityString)
        {

            Rarity rarity = Rarity.Common;

            if (!Enum.TryParse<Rarity>(rarityString, out rarity))
            {
                Debug.Log("String " + rarityString + " failed to convert");
            }
            return rarity;
        }

        // convert string to EquipmentType enum
        EquipmentType GetGearType(string gearTypeString)
        {

            EquipmentType gearType = EquipmentType.Weapon;

            if (!Enum.TryParse<EquipmentType>(gearTypeString, out gearType))
            {
                Debug.LogWarning("Converted " + gearTypeString + " failed to convert");
            }
            return gearType;
        }

        // hide/show gear items based on filters
        void UpdateFilters(ChangeEvent<string> evt)
        {
            EquipmentType gearType = GetGearType(m_InventorySlotTypeDropdown.value);
            Rarity rarity = GetRarity(m_InventoryRarityDropdown.value);
            InventoryEvents.GearFiltered?.Invoke(rarity, gearType);
        }

        // loop through the available slots and create a button for each gear item
        void ShowGearItems(List<EquipmentSO> gearToShow)
        {

            // Find the element under the ScrollView to store gear item buttons and clear existing inventory
            VisualElement contentContainer = m_ScrollViewParent.Q<VisualElement>("unity-content-container");
            contentContainer.Clear();

            for (int i = 0; i < gearToShow.Count; i++)
            {
                CreateGearItemButton(gearToShow[i], contentContainer);
            }
        }

        // generate one item for the inventory and add a clickable button to select it
        void CreateGearItemButton(EquipmentSO gearData, VisualElement container)
        {
            if (container == null)
            {
                Debug.Log("InventoryScreen.CreateGearItemButton: missing parent element");
                return;
            }

            TemplateContainer gearUIElement = m_GearItemAsset.Instantiate();
            gearUIElement.AddToClassList("gear-item-spacing");

            GearItemComponent gearItem = new GearItemComponent(gearData);

            // set visual element for gearItemComponent
            gearItem.SetVisualElements(gearUIElement);
            gearItem.SetGameData(gearUIElement);
            gearItem.RegisterButtonCallbacks();

            // add to the parent element
            container.Add(gearUIElement);
        }

        // select or deselect an item
        void SelectGearItem(GearItemComponent gearItem, bool state)
        {
            if (gearItem == null)
                return;

            m_SelectedGear = (state) ? gearItem : null;
            gearItem.CheckItem(state);
        }

        // methods to hide and show the screen
        public override void Show()
        {
            base.Show();

            InventoryEvents.ScreenEnabled?.Invoke();
            UpdateFilters(null);

            // add short transition
            m_InventoryPanel.transform.scale = new Vector3(0.1f, 0.1f, 0.1f);
            m_InventoryPanel.experimental.animation.Scale(1f, 200);
        }

        void CloseWindow(ClickEvent evt)
        {
            Hide();
        }

        public override void Hide()
        {
            base.Hide();

            AudioManager.PlayDefaultButtonSound();

            // set the selected Gear, notify the InventoryScreenController
            if (m_SelectedGear != null)
                InventoryEvents.GearSelected?.Invoke(m_SelectedGear.GearData);

            m_SelectedGear = null;

        }

        // event handling methods
        void OnInventorySetup()
        {
            SetVisualElements();
            RegisterButtonCallbacks();
        }

        // Load a list of Equipment ScriptableObjects to show in the Inventory
        void OnInventoryUpdated(List<EquipmentSO> gearToLoad)
        {
            ShowGearItems(gearToLoad);
        }

        // Add a check mark on a GearItem to show selection
        void OnGearItemClicked(GearItemComponent gearItem)
        {

            AudioManager.PlayAltButtonSound();

            // deselect previously selected
            SelectGearItem(m_SelectedGear, false);

            // select the new gear item
            SelectGearItem(gearItem, true);
        }
    }
}