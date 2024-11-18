using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIToolkitDemo
{
    [CreateAssetMenu(fileName = "InventoryChannel", menuName = "ScriptableObject/Events/InventoryChannel")]
    public class InventoryChannel : BaseEventChannelSo
    {
        public event  Action<GearItemComponent> GearItemClicked;

        public event  Action ScreenEnabled;

        public event  Action<EquipmentSO> GearSelected;

        public event  Action<Rarity, EquipmentType> GearFiltered;

        public event  Action InventorySetup;

        public event  Action<List<EquipmentSO>> InventoryUpdated;

        public event  Action<List<EquipmentSO>> GearAutoEquipped;


        public void InvokeGearItemClicked(GearItemComponent gearItem) => GearItemClicked?.Invoke(gearItem);
        public void InvokeScreenEnabled() => ScreenEnabled?.Invoke();
        public void InvokeGearSelected(EquipmentSO gear) => GearSelected?.Invoke(gear);
        public void InvokeGearFiltered(Rarity rarity, EquipmentType gearType) => GearFiltered?.Invoke(rarity, gearType);
        public void InvokeInventorySetup() => InventorySetup?.Invoke();
        public void InvokeInventoryUpdated(List<EquipmentSO> gearList) => InventoryUpdated?.Invoke(gearList);
        public void InvokeGearAutoEquipped(List<EquipmentSO> gearList) => GearAutoEquipped?.Invoke(gearList);
    }
}