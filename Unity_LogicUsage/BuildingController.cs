using UnityEngine;
using System.Collections.Generic;

/// ============ 데이터 정의 ================
/// <summary>
/// 건물 층 데이터
/// Assets 폴더에서 Create > Building > Floor Data로 생성
/// </summary>
[CreateAssetMenu(fileName="FloorData", menuName = "Building/Floor Data")]
public class FloorData:ScriptableObject
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
  [Tooltip("이전 층 대비 y축 오프셋")]
  public float heightOffset = 3f;
}

/// ============ 건물 컨트롤러 ================
/// <summary>
/// 건물의 층 관리 및 업그레이드를 담당하는 메인 컨트롤러
/// </summary>
public class BuildingController : MonoBehaviour
{
  [Header("건물 설정")]
  [Tooltip("순서대로 추가될 층 데이터들을 드래그 앤 드롭")]
  [SerializeField] private List<FloorData> floorDataList = new List<FloorData>();

  [Tooltip("층이 생성될 부모 Transform")]
  [SerializeField] private Transform floorsParent;

  [Header("현재 상태")]
  [SerializeField] private int currentFloorIndex = 0;

  private List<GameObject> instantiatedFloors = new List<GameObject>();

  private void Start()
  {
    if (floorsParent == null)
      floorsParent = transform;

    // 초기 층 생성(1층)
    if (floorDataList.Count > 0)
    {
      BuildFloor(0);
    }
  }

  /// <summary>
  /// 다음 층으로 업그레이드 시도
  /// </summary>
  public bool TryUpgrade Building(int playerMoney, int playerLevel)
  {
    // 더 이상 업그레이드 할 층이 없는지 확인
    if (currentFloorIndex >= floorDataList.Count -1)
    {
      Debug.Log("최대 층수에 도달")'
      return false;
    }

    int nextFloorIndex = currentFloorIndex + 1;
    FloorData nextFloor = floorDataList[nextFloorIndex];

    // 업그레이드 조건 확인
    if (playerMoney < nextFloor.unlockCost)
    {
      Debug.Log($"레벨 부족: Lv.{nextFloor.requiredLevel} 필요, 현재 Lv.{playerLevel}");
      return false;
    }
  }

  /// <summary>
  /// 특정 층 건설
  /// </summary>
  private void BuildFloor (int floorIndex)
  {
    if (floorIndex < 0 || floorIndex >= floorDataList.Count)
    {
      Debug.LogError($"잘못된 총 인덱스: {floorIndex}");
      return;
    }

    FloorData data = floorDataList[floorIndex];

    if (data.floorPrefab == null)
    {
      Debug.LogError($"{data.floorName}의 Prefab이 설정되지 않았습니다!");
      return;
    }

    // 층 위치 계산(이전 층들의 높이 합산)
    floor yPosition = 0f;
    for (int i = 0; i < floorIndex; i++)
    { 
      yPosition += floorDataList[i].heightOffset;
    }

    Vector3 spawnPosition = floorsParent.position + new Vector3(0, yPosition, 0);

    // 층 생성
    GameObject newFloor = Instantiate(
      data.floorPrefab;
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
    // 생성된 충돌 제거
    foreach (GameObject floor in instantiatedFloors)
    {
      if(floor != null)
        Destroy(floor);
    }

    instantiatedFloors.Clear();
    currentFloorIndex = 0;

    if (floorDataList.Count > 0)
    {
      BuildFloor(0);
    }
  }
}

/// ============ 헬퍼 클래스 ================
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

/// ============ 헬퍼 클래스 ================
/// <summary>
/// 건물 업그레이드 UI 컨트롤러(예제)
/// </summary>
public class BuildingUIController : MonoBehaviour
{
  [SerializeField] private BuildingController buildingController;
  [SerializeField] private int testPlayerMoney = 1000;
  [SerializeField] private int testPlayerLevel = 1;

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
      textPlayerLevel
    );

    if (success)
    {
      // 비용 차감
      BuildingInfo info = buildingController.GetBuildingInfo();
      if (info.nextFloorData != null)
      {
        testPlayerMoney -= info.nextFloorData.unlockCost;
      }
    }

    UpdateUI();
  }

  private void UpdateUI()
  {
    BuildingInfo info = buildingController.GetBuildingInfo();

    Debug.Log($"=== 건물 정보 ===");
    Debug.Log($"현재 층: {info.currentFloor}/{info.totalFloors}");
    Debug.Log($"플레이어 자금: {testPlayerMoney}");
            
    if (info.canUpgrade && info.nextFloorData != null)
    {
        Debug.Log($"다음 업그레이드: {info.nextFloorData.floorName}");
        Debug.Log($"필요 비용: {info.nextFloorData.unlockCost}");
        Debug.Log($"필요 레벨: {info.nextFloorData.requiredLevel}");
    }
    else
    {
        Debug.Log("더 이상 업그레이드 불가");
    }
  }
}
