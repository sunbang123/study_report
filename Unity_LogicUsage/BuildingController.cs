using UnityEngine;
using System.Collections.Generic;

// ==================== 데이터 정의 ====================
/// <summary>
/// 건물 층 데이터 (ScriptableObject)
/// Assets 폴더에서 Create > Building > Floor Data로 생성
/// </summary>
[CreateAssetMenu(fileName = "FloorData", menuName = "Building/Floor Data")]
public class FloorData : ScriptableObject
{
    [Header("층 정보")]
    [Tooltip("층 이름 (예: 1층, 2층)")]
    public string floorName;
    
    [Tooltip("이 층에 배치할 Prefab")]
    public GameObject floorPrefab;
    
    [Header("업그레이드 조건")]
    [Tooltip("이 층을 해금하는데 필요한 비용")]
    public int unlockCost;
    
    [Tooltip("필요한 플레이어 레벨")]
    public int requiredLevel;
    
    [Header("층 위치 설정")]
    [Tooltip("이전 층 대비 Y축 오프셋")]
    public float heightOffset = 3f;
}

// ==================== 이벤트 정의 ====================
/// <summary>
/// 건물 업그레이드 이벤트 델리게이트
/// </summary>
public delegate void BuildingUpgradeDelegate(FloorData newFloor, int currentFloor);

/// <summary>
/// 업그레이드 실패 이벤트 델리게이트
/// </summary>
public delegate void UpgradeFailedDelegate(string reason);

/// <summary>
/// 건물 초기화 이벤트 델리게이트
/// </summary>
public delegate void BuildingResetDelegate();

// ==================== 건물 컨트롤러 ====================
/// <summary>
/// 건물의 층 관리 및 업그레이드를 담당하는 메인 컨트롤러
/// </summary>
public class BuildingController : MonoBehaviour
{
    [Header("건물 설정")]
    [Tooltip("순서대로 추가될 층 데이터들을 드래그앤드롭")]
    [SerializeField] private List<FloorData> floorDataList = new List<FloorData>();
    
    [Tooltip("층이 생성될 부모 Transform")]
    [SerializeField] private Transform floorsParent;
    
    [Header("현재 상태")]
    [SerializeField] private int currentFloorIndex = 0;
    
    // 생성된 층 오브젝트들을 저장
    private List<GameObject> instantiatedFloors = new List<GameObject>();
    
    // ==================== 이벤트 ====================
    /// <summary>
    /// 업그레이드 성공 시 호출되는 이벤트
    /// </summary>
    public event BuildingUpgradeDelegate OnBuildingUpgraded;
    
    /// <summary>
    /// 업그레이드 실패 시 호출되는 이벤트
    /// </summary>
    public event UpgradeFailedDelegate OnUpgradeFailed;
    
    /// <summary>
    /// 건물 초기화 시 호출되는 이벤트
    /// </summary>
    public event BuildingResetDelegate OnBuildingReset;
    
    private void Start()
    {
        // 부모가 지정되지 않았으면 자기 자신을 부모로 사용
        if (floorsParent == null)
            floorsParent = transform;
        
        // 초기 층 생성 (1층)
        if (floorDataList.Count > 0)
        {
            BuildFloor(0);
        }
    }
    
    /// <summary>
    /// 다음 층으로 업그레이드 시도
    /// </summary>
    public bool TryUpgradeBuilding(int playerMoney, int playerLevel)
    {
        // 더 이상 업그레이드할 층이 없는지 확인
        if (currentFloorIndex >= floorDataList.Count - 1)
        {
            OnUpgradeFailed?.Invoke("최대 층수에 도달했습니다!");
            return false;
        }
        
        int nextFloorIndex = currentFloorIndex + 1;
        FloorData nextFloor = floorDataList[nextFloorIndex];
        
        // 업그레이드 조건 확인
        if (playerMoney < nextFloor.unlockCost)
        {
            OnUpgradeFailed?.Invoke($"자금 부족: {nextFloor.unlockCost} 필요, 현재 {playerMoney}");
            return false;
        }
        
        if (playerLevel < nextFloor.requiredLevel)
        {
            OnUpgradeFailed?.Invoke($"레벨 부족: Lv.{nextFloor.requiredLevel} 필요, 현재 Lv.{playerLevel}");
            return false;
        }
        
        // 업그레이드 실행
        BuildFloor(nextFloorIndex);
        currentFloorIndex = nextFloorIndex;
        
        // 이벤트 발생
        OnBuildingUpgraded?.Invoke(nextFloor, currentFloorIndex + 1);
        return true;
    }
    
    /// <summary>
    /// 특정 층 건설
    /// </summary>
    private void BuildFloor(int floorIndex)
    {
        if (floorIndex < 0 || floorIndex >= floorDataList.Count)
        {
            Debug.LogError($"잘못된 층 인덱스: {floorIndex}");
            return;
        }
        
        FloorData data = floorDataList[floorIndex];
        
        if (data.floorPrefab == null)
        {
            Debug.LogError($"{data.floorName}의 Prefab이 설정되지 않았습니다!");
            return;
        }
        
        // 층 위치 계산 (이전 층들의 높이 합산)
        float yPosition = 0f;
        for (int i = 0; i < floorIndex; i++)
        {
            yPosition += floorDataList[i].heightOffset;
        }
        
        Vector3 spawnPosition = floorsParent.position + new Vector3(0, yPosition, 0);
        
        // 층 생성
        GameObject newFloor = Instantiate(
            data.floorPrefab, 
            spawnPosition, 
            Quaternion.identity, 
            floorsParent
        );
        
        newFloor.name = $"{data.floorName}";
        instantiatedFloors.Add(newFloor);
        
        Debug.Log($"{data.floorName} 건설 완료 (Y: {yPosition})");
    }
    
    /// <summary>
    /// 현재 건물 정보 반환
    /// </summary>
    public BuildingInfo GetBuildingInfo()
    {
        return new BuildingInfo
        {
            currentFloor = currentFloorIndex + 1,
            totalFloors = floorDataList.Count,
            canUpgrade = currentFloorIndex < floorDataList.Count - 1,
            nextFloorData = (currentFloorIndex < floorDataList.Count - 1) 
                ? floorDataList[currentFloorIndex + 1] 
                : null
        };
    }
    
    /// <summary>
    /// 건물 초기화 (테스트용)
    /// </summary>
    [ContextMenu("건물 초기화")]
    public void ResetBuilding()
    {
        // 생성된 층들 제거
        foreach (GameObject floor in instantiatedFloors)
        {
            if (floor != null)
                Destroy(floor);
        }
        
        instantiatedFloors.Clear();
        currentFloorIndex = 0;
        
        // 1층 재생성
        if (floorDataList.Count > 0)
        {
            BuildFloor(0);
        }
        
        // 이벤트 발생
        OnBuildingReset?.Invoke();
    }
}

// ==================== 헬퍼 클래스 ====================
/// <summary>
/// 건물 정보를 담는 구조체
/// </summary>
public struct BuildingInfo
{
    public int currentFloor;
    public int totalFloors;
    public bool canUpgrade;
    public FloorData nextFloorData;
}

// ==================== UI 컨트롤러 예제 ====================
/// <summary>
/// 건물 업그레이드 UI 컨트롤러 (Delegate 패턴 사용)
/// </summary>
public class BuildingUIController : MonoBehaviour
{
    [SerializeField] private BuildingController buildingController;
    [SerializeField] private int testPlayerMoney = 1000;
    [SerializeField] private int testPlayerLevel = 1;
    
    private void OnEnable()
    {
        // 이벤트 구독
        if (buildingController != null)
        {
            buildingController.OnBuildingUpgraded += HandleBuildingUpgraded;
            buildingController.OnUpgradeFailed += HandleUpgradeFailed;
            buildingController.OnBuildingReset += HandleBuildingReset;
        }
    }
    
    private void OnDisable()
    {
        // 이벤트 구독 해제 (메모리 누수 방지)
        if (buildingController != null)
        {
            buildingController.OnBuildingUpgraded -= HandleBuildingUpgraded;
            buildingController.OnUpgradeFailed -= HandleUpgradeFailed;
            buildingController.OnBuildingReset -= HandleBuildingReset;
        }
    }
    
    private void Update()
    {
        // 테스트용: Space키로 업그레이드 시도
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnUpgradeButtonClicked();
        }
        
        // 테스트용: R키로 초기화
        if (Input.GetKeyDown(KeyCode.R))
        {
            buildingController.ResetBuilding();
        }
    }
    
    public void OnUpgradeButtonClicked()
    {
        bool success = buildingController.TryUpgradeBuilding(
            testPlayerMoney, 
            testPlayerLevel
        );
        
        // 성공 시 비용 차감은 이벤트 핸들러에서 처리
    }
    
    // ==================== 이벤트 핸들러 ====================
    
    /// <summary>
    /// 업그레이드 성공 시 호출되는 핸들러
    /// </summary>
    private void HandleBuildingUpgraded(FloorData newFloor, int currentFloor)
    {
        // 비용 차감
        testPlayerMoney -= newFloor.unlockCost;
        
        // UI 업데이트
        Debug.Log($"업그레이드 완료: {newFloor.floorName} 건설!");
        Debug.Log($"비용 차감: -{newFloor.unlockCost} (잔액: {testPlayerMoney})");
        
        UpdateUI();
    }
    
    /// <summary>
    /// 업그레이드 실패 시 호출되는 핸들러
    /// </summary>
    private void HandleUpgradeFailed(string reason)
    {
        Debug.LogWarning($"❌ 업그레이드 실패: {reason}");
        UpdateUI();
    }
    
    /// <summary>
    /// 건물 초기화 시 호출되는 핸들러
    /// </summary>
    private void HandleBuildingReset()
    {
        Debug.Log("🔄 건물이 초기화되었습니다!");
        UpdateUI();
    }
    
    /// <summary>
    /// UI 정보 갱신
    /// </summary>
    private void UpdateUI()
    {
        BuildingInfo info = buildingController.GetBuildingInfo();
        
        Debug.Log($"=== 건물 정보 ===");
        Debug.Log($"현재 층: {info.currentFloor}/{info.totalFloors}");
        Debug.Log($"플레이어 자금: {testPlayerMoney}");
        Debug.Log($"플레이어 레벨: {testPlayerLevel}");
        
        if (info.canUpgrade && info.nextFloorData != null)
        {
            Debug.Log($"다음 업그레이드: {info.nextFloorData.floorName}");
            Debug.Log($"   필요 비용: {info.nextFloorData.unlockCost}");
            Debug.Log($"   필요 레벨: {info.nextFloorData.requiredLevel}");
        }
        else
        {
            Debug.Log("최대 층수 도달! 더 이상 업그레이드 불가");
        }
    }
}
