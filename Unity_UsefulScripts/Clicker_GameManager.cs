using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Clicker_GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Economy")]
    [SerializeField] private long money;
    [SerializeField] private long moneyIncreaseAmount;
    [SerializeField] private long moneyIncreaseLevel;
    [SerializeField] private long moneyIncreasePrice;

    [Header("UI References")]
    [SerializeField] private Text textMoney;
    [SerializeField] private Text textEmployee;
    [SerializeField] private GameObject panelPrice;
    [SerializeField] private GameObject panelRecruit;
    [SerializeField] private Button buttonPrice;
    [SerializeField] private Button buttonRecruit;

    [Header("Prefabs")]
    [SerializeField] private GameObject prefabMoney;
    [SerializeField] private GameObject prefabEmployee;
    [SerializeField] private GameObject prefabFloor;

    [Header("Employee Settings")]
    [SerializeField] private int employeeCount;
    [SerializeField] private long employeeIncreaseAmount;
    [SerializeField] private long employeePrice;
    [SerializeField] private int width = 6;
    [SerializeField] private float space = 1f;

    [Header("Floor Settings")]
    [SerializeField] private float spaceFloor = 5f;
    [SerializeField] private int floorCapacity = 6;
    private int currentFloor = 1;

    private const string SAVE_FILE_NAME = "/save.xml";
    private Camera mainCamera;
    private Transform bossTransform;

    #region Unity Lifecycle

    private void Awake()
    {
        InitializeSingleton();
        CacheReferences();
    }

    private void Start()
    {
        LoadGameData();
        FillEmployee();
    }

    private void Update()
    {
        UpdateUI();
        HandleInput();
        UpdateButtons();
        CheckFloorCreation();
    }

    #endregion

    #region Initialization

    private void InitializeSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void CacheReferences()
    {
        mainCamera = Camera.main;
        GameObject boss = GameObject.Find("Boss");
        if (boss != null)
            bossTransform = boss.transform;
    }

    #endregion

    #region Save/Load

    private void LoadGameData()
    {
        string path = GetSavePath();
        
        if (System.IO.File.Exists(path))
        {
            LoadFromXml();
        }
        else
        {
            LoadFromPlayerPrefs();
        }
    }

    private void LoadFromXml()
    {
        SaveData saveData = XmlManager.XmlLoad<SaveData>(GetSavePath());
        ApplySaveData(saveData);
    }

    private void LoadFromPlayerPrefs()
    {
        string moneyString = PlayerPrefs.GetString("MONEY", "0");
        money = long.Parse(moneyString);
    }

    private void ApplySaveData(SaveData saveData)
    {
        money = saveData.money;
        moneyIncreaseAmount = saveData.moneyIncreaseAmount;
        moneyIncreaseLevel = saveData.moneyIncreaseLevel;
        moneyIncreasePrice = saveData.moneyIncreasePrice;
        employeeCount = saveData.employeeCount;
    }

    public void Save()
    {
        SaveData saveData = CreateSaveData();
        XmlManager.XmlSave(saveData, GetSavePath());
    }

    private SaveData CreateSaveData()
    {
        return new SaveData
        {
            money = money,
            moneyIncreaseAmount = moneyIncreaseAmount,
            moneyIncreaseLevel = moneyIncreaseLevel,
            moneyIncreasePrice = moneyIncreasePrice,
            employeeCount = employeeCount
        };
    }

    private string GetSavePath()
    {
        return Application.persistentDataPath + SAVE_FILE_NAME;
    }

    #endregion

    #region Input Handling

    private void HandleInput()
    {
        HandleMoneyClick();
        HandleQuitInput();
    }

    private void HandleMoneyClick()
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUI())
        {
            IncreaseMoney();
        }
    }

    private void HandleQuitInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Save();
            Application.Quit();
        }
    }

    private bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    #endregion

    #region Money System

    private void IncreaseMoney()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(prefabMoney, mousePosition, Quaternion.identity);
        money += moneyIncreaseAmount;
    }

    public void UpgradeMoneyIncrease()
    {
        if (money < moneyIncreasePrice) return;

        money -= moneyIncreasePrice;
        moneyIncreaseLevel++;
        moneyIncreaseAmount = moneyIncreaseLevel * 100;
        moneyIncreasePrice *= 500;
    }

    #endregion

    #region Employee System

    private void FillEmployee()
    {
        GameObject[] employees = GameObject.FindGameObjectsWithTag("Employee");
        int currentEmployeeCount = employees.Length;

        if (employeeCount > currentEmployeeCount)
        {
            for (int i = currentEmployeeCount; i < employeeCount; i++)
            {
                CreateEmployeeAtIndex(i);
            }
        }
    }

    public void Recruit()
    {
        if (money < AutoWork.autoIncreasePrice) return;

        money -= AutoWork.autoIncreasePrice;
        employeeCount++;
        
        AutoWork.autoMoneyIncreaseAmount += moneyIncreaseLevel * 5;
        AutoWork.autoIncreasePrice += employeeCount * 500;

        CreateEmployee();
    }

    private void CreateEmployee()
    {
        CreateEmployeeAtIndex(employeeCount - 1);
    }

    private void CreateEmployeeAtIndex(int index)
    {
        if (bossTransform == null) return;

        Vector2 position = CalculateEmployeePosition(index);
        Instantiate(prefabEmployee, position, Quaternion.identity);
    }

    private Vector2 CalculateEmployeePosition(int index)
    {
        Vector2 bossSpot = bossTransform.position;
        float spotX = bossSpot.x + (index % width) * space;
        float spotY = bossSpot.y - (index / width) * space;
        return new Vector2(spotX, spotY);
    }

    #endregion

    #region Floor System

    private void CheckFloorCreation()
    {
        int nextFloorThreshold = currentFloor * floorCapacity;
        
        if (employeeCount >= nextFloorThreshold)
        {
            CreateNewFloor();
        }
    }

    private void CreateNewFloor()
    {
        GameObject background = GameObject.Find("Background");
        if (background == null) return;

        Vector2 bgPosition = background.transform.position;
        float spotY = bgPosition.y - (spaceFloor * currentFloor);
        Vector2 floorPosition = new Vector2(bgPosition.x, spotY);

        Instantiate(prefabFloor, floorPosition, Quaternion.identity);
        currentFloor++;

        UpdateCameraLimit();
    }

    private void UpdateCameraLimit()
    {
        CameraDrag cameraDrag = mainCamera.GetComponent<CameraDrag>();
        if (cameraDrag != null)
        {
            cameraDrag.limitMinY -= spaceFloor;
        }
    }

    #endregion

    #region UI Updates

    private void UpdateUI()
    {
        UpdateMoneyText();
        UpdateEmployeeText();
        UpdatePanels();
    }

    private void UpdateMoneyText()
    {
        textMoney.text = money == 0 ? "0" : $"{money:###,###}원";
    }

    private void UpdateEmployeeText()
    {
        if (employeeCount > 0)
        {
            textEmployee.text = $"{employeeCount:###,###}명";
        }
    }

    private void UpdateButtons()
    {
        buttonPrice.interactable = money >= moneyIncreasePrice;
        buttonRecruit.interactable = money >= AutoWork.autoIncreasePrice;
    }

    private void UpdatePanels()
    {
        if (panelPrice.activeSelf)
            UpdateUpgradePanel();
        
        if (panelRecruit.activeSelf)
            UpdateRecruitPanel();
    }

    private void UpdateUpgradePanel()
    {
        Text textPrice = panelPrice.transform.Find("Text").GetComponent<Text>();
        if (textPrice == null) return;

        textPrice.text = $"Lv.{moneyIncreaseLevel} 클릭 강화\n\n" +
                        $"현재 클릭 수익>\n{moneyIncreaseAmount:###,###}원\n" +
                        $"업그레이드 비용>\n{moneyIncreasePrice:###,###}원\n";
    }

    private void UpdateRecruitPanel()
    {
        Text textRecruit = panelRecruit.transform.Find("Text").GetComponent<Text>();
        if (textRecruit == null) return;

        textRecruit.text = $"Lv.{employeeCount} 직원 고용\n\n" +
                          $"현재 자동 클릭 수익>\n{employeeIncreaseAmount:###,###}원\n" +
                          $"업그레이드 비용>\n{employeePrice:###,###}원\n";
    }

    public void TogglePanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(!panel.activeSelf);
        }
    }

    #endregion

    #region Public Properties

    public long Money => money;
    public long MoneyIncreaseAmount => moneyIncreaseAmount;
    public int EmployeeCount => employeeCount;

    #endregion
}
