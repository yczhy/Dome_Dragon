using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIToolkitDemo
{
    [CreateAssetMenu(fileName = "ShopChannel", menuName = "ScriptableObject/Events/ShopChannel")]
    public class ShopChannel : BaseEventChannelSo
    {
        public  event Action GoldSelected; // 购买金币

        public  event Action GemSelected; // 购买宝石

        public  event Action PotionSelected; // 购买药水

        public  event Action<string> TabSelected; // 选择标签页

        public  event Action<ShopItemSO, Vector2> ShopItemClicked; // 点击商店物品

        public  event Action<ShopItemSO, Vector2> ShopItemPurchasing; // 购买商店物品

        public  event Action<List<ShopItemSO>> ShopUpdated; // 商店更新

        public  event Action<GameData> PotionsUpdated; // 药水更新

        public  event Action<GameData> FundsUpdated; // 资金更新

        public  event Action<ShopItemType, uint, Vector2> RewardProcessed; // 奖励处理

        public  event Action<ShopItemSO, Vector2> TransactionProcessed; // 交易处理

        public  event Action<ShopItemSO> TransactionFailed; // 交易失败

        public  void InvokeGoldSelected() => GoldSelected?.Invoke();
        public  void InvokeGemSelected() => GemSelected?.Invoke();
        public  void InvokePotionSelected() => PotionSelected?.Invoke();
        public  void InvokeTabSelected(string tabName) => TabSelected?.Invoke(tabName);
        public  void InvokeShopItemClicked(ShopItemSO item, Vector2 position) => ShopItemClicked?.Invoke(item, position);
        public  void InvokeShopItemPurchasing(ShopItemSO item, Vector2 position) => ShopItemPurchasing?.Invoke(item, position);
        public  void InvokeShopUpdated(List<ShopItemSO> items) => ShopUpdated?.Invoke(items);
        public  void InvokePotionsUpdated(GameData data) => PotionsUpdated?.Invoke(data);
        public  void InvokeFundsUpdated(GameData data) => FundsUpdated?.Invoke(data);
        public  void InvokeRewardProcessed(ShopItemType type, uint amount, Vector2 position) => RewardProcessed?.Invoke(type, amount, position);
        public  void InvokeTransactionProcessed(ShopItemSO item, Vector2 position) => TransactionProcessed?.Invoke(item, position);
        public  void InvokeTransactionFailed(ShopItemSO item) => TransactionFailed?.Invoke(item);
    }
}