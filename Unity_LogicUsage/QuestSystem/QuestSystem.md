```
sequenceDiagram
    actor Player
    participant NPC
    participant QM as QuestManager
    participant Val as Validators
    participant Quest
    participant Reward as RewardHandlers
    participant Game as GameSystems

    Note over Player,Game: 1. 퀘스트 수락 과정
    Player->>NPC: 대화 시작
    NPC->>QM: AcceptQuest("quest_001")
    QM->>QM: allQuests에서 QuestData 찾기
    
    QM->>Val: ValidateQuest(questData)
    Val->>Val: DuplicateQuestValidator.Validate()
    Val->>Val: PrerequisiteQuestValidator.Validate()
    Val->>Val: LevelRequirementValidator.Validate()
    Val-->>QM: (true, "")
    
    QM->>Quest: new Quest(questData)
    Quest-->>QM: quest instance
    QM->>QM: activeQuests.Add(quest)
    QM->>QM: OnQuestAccepted?.Invoke(quest)
    QM-->>NPC: true
    NPC-->>Player: "퀘스트를 수락했습니다!"

    Note over Player,Game: 2. 퀘스트 진행 과정
    Player->>Game: 적 처치
    Game->>QM: UpdateQuestProgress(Kill, "goblin", 1)
    
    loop 모든 활성 퀘스트
        QM->>Quest: quest.UpdateObjective(Kill, "goblin", 1)
        Quest->>Quest: currentAmount += 1
        Quest-->>QM: 업데이트 완료
        QM->>QM: OnQuestProgressed?.Invoke(quest)
        
        QM->>Quest: quest.IsCompleted()?
        alt 퀘스트 완료
            Quest-->>QM: true
            QM->>Quest: quest.state = Completed
            QM->>QM: OnQuestCompleted?.Invoke(quest)
        else 아직 진행 중
            Quest-->>QM: false
        end
    end
    
    QM-->>Game: 업데이트 완료
    Game-->>Player: UI 업데이트

    Note over Player,Game: 3. 퀘스트 보상 받기
    Player->>NPC: 퀘스트 완료 보고
    NPC->>QM: TurnInQuest("quest_001")
    
    QM->>QM: CanTurnInQuest(quest)?
    alt 완료된 퀘스트
        QM->>Reward: ProcessRewards(quest.rewards)
        
        loop 각 보상
            Reward->>Reward: rewardHandlers.TryGetValue(type)
            alt Gold
                Reward->>Game: GoldRewardHandler.GiveReward()
                Game-->>Player: 💰 골드 획득
            else Experience
                Reward->>Game: ExperienceRewardHandler.GiveReward()
                Game-->>Player: ⭐ 경험치 획득
            else Item
                Reward->>Game: ItemRewardHandler.GiveReward()
                Game-->>Player: 🎁 아이템 획득
            end
        end
        
        QM->>QM: CompleteQuestTurnIn(quest)
        QM->>QM: activeQuests.Remove(quest)
        QM->>QM: completedQuests.Add(quest)
        QM->>QM: OnQuestTurnedIn?.Invoke(quest)
        QM-->>NPC: true
        NPC-->>Player: "보상을 받았습니다!"
    else 미완료 퀘스트
        QM-->>NPC: false
        NPC-->>Player: "아직 완료하지 못했습니다"
    end
```
