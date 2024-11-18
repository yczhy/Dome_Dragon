using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIToolkitDemo
{
    [CreateAssetMenu(fileName = "CharChannel", menuName = "ScriptableObject/Events/CharChannel")]
    public class CharChannel : BaseEventChannelSo
    {
        # region 按钮事件

        public event Action CharScreenStarted;

        public event Action CharScreenEnded;

        public event Action<int> InventoryOpened; // 从指定 装备槽 打开 装备列表

        public event Action<int> NextOrLastCharacterSelected; // 选择上一个或者下一个角色

        public event Action GearAutoEquipped; // 自动装备

        public event Action GearAllUnequipped; // 全部卸下

        public event Action LevelUpClicked; // 点击升级按钮

        #endregion

        // --------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------

        # region 角色数据事件

        public event Action<bool> LevelUpButtonEnabled; // 是否启用升级按钮

        public event Action<bool> CharacterLeveledUp; // 角色是否升级成功

        public event Action<CharacterData> LevelIncremented; // 角色升级

        public event Action<float> LevelUpdated; // 更新等级计量器

        public event Action PreviewInitialized; // 预览初始化

        public event Action<CharacterData> CharacterShown; // 显示一个角色时触发

        public event Action<EquipmentSO> GearItemUnequipped; // 卸下装备

        public event Action<EquipmentSO, int> GearSlotUpdated; // 更新装备槽

        public event Action<CharacterData> CharacterAutoEquipped; // 传入 角色信息 穿戴装备

        public event Action<List<CharacterData>> GearDataInitialized; // 初始化 所有角色的数据

        public event Action<CharacterData> LevelPotionUsed; // 使用等级药水

        public Func<LevelMeterData> GetLevelMeterData; // 获取等级计量器数据

        #endregion

        #region 按钮事件

        // 触发 CharScreenStarted 事件
        public void InvokeCharScreenStarted()
        {
            CharScreenStarted?.Invoke();
        }

        // 触发 CharScreenEnded 事件
        public void InvokeCharScreenEnded()
        {
            CharScreenEnded?.Invoke();
        }

        // 触发 InventoryOpened 事件
        public void InvokeInventoryOpened(int slotIndex)
        {
            InventoryOpened?.Invoke(slotIndex);
        }

        // 触发 NextOrLastCharacterSelected 事件
        public void InvokeNextOrLastCharacterSelected(int direction)
        {
            NextOrLastCharacterSelected?.Invoke(direction);
        }

        // 触发 GearAutoEquipped 事件
        public void InvokeGearAutoEquipped()
        {
            GearAutoEquipped?.Invoke();
        }

        // 触发 GearAllUnequipped 事件
        public void InvokeGearAllUnequipped()
        {
            GearAllUnequipped?.Invoke();
        }

        // 触发 LevelUpClicked 事件
        public void InvokeLevelUpClicked()
        {
            LevelUpClicked?.Invoke();
        }

        #endregion

        // --------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------

        #region 角色数据事件

        // 触发 LevelUpButtonEnabled 事件
        public void InvokeLevelUpButtonEnabled(bool isEnabled)
        {
            LevelUpButtonEnabled?.Invoke(isEnabled);
        }

        // 触发 CharacterLeveledUp 事件
        public void InvokeCharacterLeveledUp(bool success)
        {
            CharacterLeveledUp?.Invoke(success);
        }

        // 触发 LevelIncremented 事件
        public void InvokeLevelIncremented(CharacterData characterData)
        {
            LevelIncremented?.Invoke(characterData);
        }

        // 触发 LevelUpdated 事件
        public void InvokeLevelUpdated(float levelProgress)
        {
            LevelUpdated?.Invoke(levelProgress);
        }

        // 触发 PreviewInitialized 事件
        public void InvokePreviewInitialized()
        {
            PreviewInitialized?.Invoke();
        }

        // 触发 CharacterShown 事件
        public void InvokeCharacterShown(CharacterData characterData)
        {
            CharacterShown?.Invoke(characterData);
        }

        // 触发 GearItemUnequipped 事件
        public void InvokeGearItemUnequipped(EquipmentSO gear)
        {
            GearItemUnequipped?.Invoke(gear);
        }

        // 触发 GearSlotUpdated 事件
        public void InvokeGearSlotUpdated(EquipmentSO gear, int slotIndex)
        {
            GearSlotUpdated?.Invoke(gear, slotIndex);
        }

        // 触发 CharacterAutoEquipped 事件
        public void InvokeCharacterAutoEquipped(CharacterData characterData)
        {
            CharacterAutoEquipped?.Invoke(characterData);
        }

        // 触发 GearDataInitialized 事件
        public void InvokeGearDataInitialized(List<CharacterData> characterDataList)
        {
            GearDataInitialized?.Invoke(characterDataList);
        }

        // 触发 LevelPotionUsed 事件
        public void InvokeLevelPotionUsed(CharacterData characterData)
        {
            LevelPotionUsed?.Invoke(characterData);
        }

        #endregion
    }
}