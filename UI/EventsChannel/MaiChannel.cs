using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIToolkitDemo
{
    [CreateAssetMenu(fileName = "MailChannel", menuName = "ScriptableObject/Events/CharChannel")]
    public class MailChannel : BaseEventChannelSo
    {
        public event Action<string> TabSelected; // 选择 收件箱 或 垃圾箱 的tab

        public event Action<List<MailMessageSO>> MailboxUpdated; // 收件箱更新

        public event Action<int> MarkedAsRead; // 标记为已读

        public event Action<int> MessageSelected; // 选择某封邮件

        public event Action ShowEmptyMessage; // 显示空消息

        public event Action<MailMessageSO> MessageShown; // 显示某封邮件

        public event Action<MailMessageSO, Vector2> RewardClaimed; // 领取奖励

        public event Action DeleteClicked; // 删除邮件

        public event Action<int> MessageDeleted; // 删除某封邮件

        public event Action<int> UndeleteClicked; // 恢复邮件

        public event Action<int, Vector2> ClaimRewardClicked; // 领取奖励

        public void InvokeTabSelected(string tabName) => TabSelected?.Invoke(tabName);
        public void InvokeMailboxUpdated(List<MailMessageSO> messages) => MailboxUpdated?.Invoke(messages);
        public void InvokeMarkedAsRead(int index) => MarkedAsRead?.Invoke(index);
        public void InvokeMessageSelected(int index) => MessageSelected?.Invoke(index);
        public void InvokeShowEmptyMessage() => ShowEmptyMessage?.Invoke();
        public void InvokeMessageShown(MailMessageSO message) => MessageShown?.Invoke(message);
        public void InvokeRewardClaimed(MailMessageSO message, Vector2 screenPos) => RewardClaimed?.Invoke(message, screenPos);
        public void InvokeDeleteClicked() => DeleteClicked?.Invoke();
        public void InvokeMessageDeleted(int index) => MessageDeleted?.Invoke(index);
        public void InvokeUndeleteClicked(int index) => UndeleteClicked?.Invoke(index);
        public void InvokeClaimRewardClicked(int index, Vector2 screenPos) => ClaimRewardClicked?.Invoke(index, screenPos);
    }
}