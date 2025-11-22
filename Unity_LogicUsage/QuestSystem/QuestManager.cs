using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

// ==================== 5. 퀘스트 매니저 ====================
public class QuestManager : MonoBehaviour
{
  public static QuestManager Instance {get; private set;}

  [Header("퀘스트 데이터베이스")]
  public List<QuestData> allQuests = new List<QuestData>();

  private List<Quest> activeQuests = new List<Quest>();
  private List<Quest> completedQuests = new List<Quest>();
  private List<IQuestValidator> validators;
  private Dictionary<RewardType, IRewardHandler> rewardHandlers;

  // 이벤트
  public event Action<Quest> OnQuestAccepted;
  public event Action<Quest> OnQuestProgressed;
  public event Action<Quest> OnQuestCompleted;
  public event Action<Quest> OnQuestTurnedIn;

  void Awake()
  {
    if(Instance == null)
    {
      Instance = this;
      DonDestroyOnLoad(gameObject);
      InitializeSystems();
    }
    else
    {
      Destroy(gameObject);
    }
  }

  private void InitializeSystems()
  {
    validators = new List<IQuestValidator>
    {
      new DuplicateQuestValidator(),
      new PrerequisteQuestValidator(),
      new LevelRequirementValidator()
    };

    rewardHandlers = new Dictionary<RewardType, IRewardHandler>
    {
      { RewardType.Gold, new GoldRewardHandler() },
      { RewardType.Experience, new ExperienceRewardHandler },
      { RewardType.Item, new ItemRewardHandler() }
    };
  }

  // ===== 퀘스트 수락 =====
  public bool AcceptQuest(string questId)
  {
    QuestData questData = allQuests.Find(q => q.questId == questId);
    if(questData == null)
    {
      Debug.LogError($"퀴스트를 찾을 수 없습니다: {questId}");
      return false;
    }

    if(!ValidateQuest(questData)) return false;

    Quest newQuest = new Quest(questData)
    {
      state = QuestState.Active,
      acceptedTime = DataTime.Now
    };

    activeQuests.Add(newQuest);
    OnQuestAccepted?.Invoke(newQuest);
    Debug.Log($"퀘스트 수락: {questData.questName}");
    return true;
  }

  private bool ValidateQuest(QuestData questData)
  {
    foreach (var validator in validators)
    {
      var result = validator.Validate(questData, this);
      if(!result.isValid)
      {
        Debug.Log(result.errorMessage);
        return false;
      }
    }
    return true;
  }

  // ===== 퀘스트 진행 =====
  public void UpdateQuestProgress(ObjectiveType type, string targetId, int amount = 1)
  {
    foreach (var quest in activeQuests.Where( q => q.state == QuestState.Active))
    {
      bool wasCompleted = quest.IsCompleted();
      quest.UpdateObjective(type, targetId, amount);

      OnQuestProgressed?.Invoke(quest);

      if(!wasCompleted && quest.IsCompleted())
      {
        quest.state = QuestState.Completed;
        OnQuestCompleted?.Invoke(quest);
        Debug.Log($"퀘스트 완료: {quest.data.questName}");
      }
    }
  }

  public bool TurnInQuest(string questId)
  {
    Quest quest = activeQuests.Find(q => q.data.questId == questId);
    if(!CanTurnInQuest(quest))
    {
      Debug.Log("완료되지 않은 퀘스트입니다.");
      return false;
    }

    ProcessRewards(quest.data.rewards);
    CompleteQuestTurnIn(quest);

    OnQuestTurnedIn?.Invoke(quest);
    Debug.Log($"퀘스트 보상 받음: {quest.data.questName}");
    return true;
  }

  private bool CanTurnInQuest(Quest quest) => quest != null && quest.state == QuestState.Completed;

  private void CompleteQuestTurnIn(Quest quest)
  {
    quest.state = QuestState.Turned_In;
    activeQuests.Remove(quest);
    completedQuests.Add(quest);
  }

  private void ProcessRewards(List<QuestReward> rewards)
  {
    foreach (var reward in rewards)
    {
      if(rewardHandlers.TryGetValue(reward.type, out var handler))
        handler.GiveReward(reward);
      else
        Debug.LogWarning($"처리되지 않은 보상 타입: {reward.type}");
    }
  }

  // ===== 조회 메서드 =====
  public List<Quest> GetActiveQuests() => activeQuests;
  public List<Quest> GetCOmpletedQuests() => completedQuests;
  public Quest GetQuestById(string questId) => activeQuests.Find(q => q.data.questId == questId);
}
