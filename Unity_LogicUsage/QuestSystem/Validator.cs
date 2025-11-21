using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

// ==================== 3. 검증 시스템 ====================

public interface IQuestValidator
{
    (bool isValid, string errorMessage) Validate(QuestData questData, QuestManager manager);
}

public class DuplicateQuestValidator : IQuestValidator
{
    public (bool isValid, string errorMessage) Validate(QuestData questData, QuestManager manager)
    {
        bool isDuplicate = manager.GetActiveQuests().Any(q => q.data.questId == questData.questId) ||
                          manager.GetCompletedQuests().Any(q => q.data.questId == questData.questId);
        
        return isDuplicate ? (false, "이미 진행 중이거나 완료한 퀘스트입니다.") : (true, string.Empty);
    }
}

public class PrerequisiteQuestValidator : IQuestValidator
{
    public (bool isValid, string errorMessage) Validate(QuestData questData, QuestManager manager)
    {
        if (questData.prerequisiteQuestIds == null || questData.prerequisiteQuestIds.Count == 0)
            return (true, string.Empty);
        
        bool hasAllPrerequisites = questData.prerequisiteQuestIds.All(prereqId =>
            manager.GetCompletedQuests().Any(q => q.data.questId == prereqId));
        
        return hasAllPrerequisites ? (true, string.Empty) : (false, "선행 퀘스트를 완료해야 합니다.");
    }
}

public class LevelRequirementValidator : IQuestValidator
{
    public (bool isValid, string errorMessage) Validate(QuestData questData, QuestManager manager)
    {
        int playerLevel = 10; // 실제로는 PlayerStats.Instance.GetLevel()
        return playerLevel >= questData.requiredLevel 
            ? (true, string.Empty) 
            : (false, $"레벨 {questData.requiredLevel} 이상 필요합니다.");
    }
}
