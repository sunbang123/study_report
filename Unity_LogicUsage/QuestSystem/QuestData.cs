using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

// ============== 1. 데이터 구조 =================

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest System/Quest Data")]
public class QuestData : ScriptableObject
{
    public string questId;
    public string questName;
    [TextArea(3,5)] public string description;
    public QuestType questType;
    public List<QuestObjective> objectives;
    public List<QuestReward> rewards;
    public int requiredLevel = 1;
    public List<string> prerequisiteQuestIds'
}

[System.Serializable]
public enum QuestType {Main, Side, Daily, Achievement}

[System.Serializable]
public class QuestObjective
{
    public ObjectiveType type;
    public string targetId;
    public string desciption;
    public int requiredAmount;
    public int currentAmount;
    public bool IsCompleted() => currentAmount >= requiredAmount;
}

[System.Serializable]
public enum ObjectiveType { Kill, Collect, Interact, Reach, Escort, Deliver }

[System.Serializable]
public class QuestReward
{
    public RewardType type;
    public string rewardId;
    public int amount;
}

[System.Serializable]
public enum RewardType { Gold, Experience, Item }
public enum QuestState { Available, Active, Completed, Turned_In }
