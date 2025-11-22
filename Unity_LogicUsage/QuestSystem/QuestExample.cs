using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class QuestExample : MonoBehaviour
{
  void Start()
  {
    QuestManager.Instance.OnQuestAccepted += q => Debug.Log($"새 퀘스트: {q.data.questName}");
    QuestManager.Instance.OnQuestCompleted += q => Debug.Log($"완료: {q.data.questName}");
  }

  // NPC와 대화
  public void TalkToQuestGiver() => QuestManager.Instance.AcceptQuest("quest_001");

  // 적 처치
  public void OnEnemyKilled(string enemyId) => QuestManager.Instance.UpdateQuestProgress(ObjectiveType.Kill, enemyId, 1);

  // 아이템 휙득
  public void OnItemCollected(string itemId) => QuestManager.Instance.UpdateQuestProgress(ObjectiveType.Collect, itemId, 1);

  // 보상 받기
  public void TurnInQuest(string questId) => QuestManager.Instance.TurnInQuest(questId);
}
