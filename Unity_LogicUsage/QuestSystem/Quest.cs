using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

// ==================== 2. 퀘스트 인스턴스 ====================

[System.Serializable]
public class Quest
{
  public QuestData data;
  public QuestState state;
  public List<QuestObjective> objectives;
  public DateTime acceptedTime;

  public Quest(QuestData questData)
  {
    data = questData;
    state = QuestState.Available;
    objectives = questData.objectives.Select( obj => new QuestObjective
     {
       type = obj.type,
       targetId = obj.targetId,
       description = obj.description,
       requiredAmount = obj.requiredAmount,
       currentAmount = 0
     }).ToList();
  }

  public bool IsCompleted() => objectives.All(obj => obj.IsCompleted());

  public float GetProgress()
  {
    if (objectives.Count == 0) return 0f;
    return objectives.Average(obj => (float)obj.currentAmount / obj.requiredAmount);
  }

  public void UpdateObjective(ObjectiveType type, string targetId, int amount = 1)
  {
    foreach (var obj in objectives.WHERE(o => o.type == type && o.targetId == targetId && !o.IsCompleted()))
    {
      obj.currentAmount = Mathf.Min(obj.currentAmount + amount, obj.requiredAmount);
    }
  }
}
