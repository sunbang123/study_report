using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest System/Quest Data")]
public class QuestData : ScriptableObject
{
    [Header("기본 정보")]
    public string questId;
    public string questName;
    [TextArea(3, 5)]
    public string description;
    
    [Header("퀘스트 타입")]
    public QuestType questType;
    
    [Header("목표")]
    public List<QuestObjective> objectives;
    
    [Header("보상")]
    public List<QuestReward> rewards;
    
    [Header("조건")]
    public int requiredLevel = 1;
    public List<string> prerequisiteQuestIds; // 선행 퀘스트
}

[System.Serializable]
public enum QuestType
{
    Main,       // 메인 퀘스트
    Side,       // 사이드 퀘스트
    Daily,      // 일일 퀘스트
    Achievement // 업적
}

[System.Serializable]
public class QuestObjective
{
    public ObjectiveType type;
    public string targetId;      // 적 ID, 아이템 ID 등
    public string description;
    public int requiredAmount;
    public int currentAmount;
    
    public bool IsCompleted() => currentAmount >= requiredAmount;
}

[System.Serializable]
public enum ObjectiveType
{
    Kill,           // 적 처치
    Collect,        // 아이템 수집
    Interact,       // NPC 대화 또는 오브젝트 상호작용
    Reach,          // 특정 위치 도달
    Escort,         // 호위
    Deliver         // 아이템 전달
}

[System.Serializable]
public class QuestReward
{
    public RewardType type;
    public string rewardId;
    public int amount;
}

[System.Serializable]
public enum RewardType
{
    Gold,
    Experience,
    Item
}
