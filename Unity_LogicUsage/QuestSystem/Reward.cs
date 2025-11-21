using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
// ==================== 4. 보상 시스템 ====================

public interface IRewardHandler
{
    void GiveReward(QuestReward reward);
}

public class GoldRewardHandler : IRewardHandler
{
    public void GiveReward(QuestReward reward)
    {
        // PlayerInventory.Instance.AddGold(reward.amount);
        Debug.Log($"💰 골드 획득: {reward.amount}");
    }
}

public class ExperienceRewardHandler : IRewardHandler
{
    public void GiveReward(QuestReward reward)
    {
        // PlayerStats.Instance.AddExperience(reward.amount);
        Debug.Log($"⭐ 경험치 획득: {reward.amount}");
    }
}

public class ItemRewardHandler : IRewardHandler
{
    public void GiveReward(QuestReward reward)
    {
        // PlayerInventory.Instance.AddItem(reward.rewardId, reward.amount);
        Debug.Log($"🎁 아이템 획득: {reward.rewardId} x{reward.amount}");
    }
}
