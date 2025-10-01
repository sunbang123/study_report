using UnityEngine;

public class UnderWater_GameMan : MonoBehaviour
{
    public GameObject titlePanel;
    public GameObject gamePanel;
    public GameObject gameOverPanel;
    public static UnderWater_GameMan instance;
    private float titlePosition = 8f;
    private float gamePosition = -0.5f;
    private GameObject currentFadeOutPanel;
    private GameObject currentFadeInPanel;

    private void Start()
    {
        instance = this.GetComponent<UnderWater_GameMan>();
        // 이벤트 구독 등록
        PotionController.OnPotionCollected += HandlePotionCollected;
    }

    private void OnDestroy()
    {
        // 메모리 누수 방지를 위한 구독 해제
        PotionController.OnPotionCollected -= HandlePotionCollected;
    }
    private void HandlePotionCollected()
    {
        Debug.Log("포션 획득! 모든 물고기 행동 변경 시작");

        // Forever_Avoid 컴포넌트를 가진 모든 오브젝트 찾기
        Forever_Avoid[] allAvoidScripts = FindObjectsOfType<Forever_Avoid>();

        foreach (Forever_Avoid avoidScript in allAvoidScripts)
        {
            Forever_Chase chaseScript = avoidScript.GetComponent<Forever_Chase>();

            if (chaseScript != null)
            {
                avoidScript.enabled = false;
                chaseScript.enabled = true;
            }
        }

        Invoke("ResetAllFishBehavior", 2f);
    }

    private void ResetAllFishBehavior()
    {
        Debug.Log("모든 물고기 행동 원래대로 복구");

        Forever_Chase[] allChaseScripts = FindObjectsOfType<Forever_Chase>();

        foreach (Forever_Chase chaseScript in allChaseScripts)
        {
            Forever_Avoid avoidScript = chaseScript.GetComponent<Forever_Avoid>();

            if (avoidScript != null)
            {
                avoidScript.enabled = true;
                chaseScript.enabled = false;
            }
        }
    }
    public void GameStart()
    {
        // 카메라 추적 비활성화
        Forever_ChaseCameraH.instance.isEnabled = false;

        InvokeRepeating("MoveCameraDown", 0f, 0.02f);
        Hide(titlePanel);
        Show(gamePanel);
    }

    public void Hide(GameObject targetPanel)
    {
        CancelInvoke("FadeOut");
        currentFadeOutPanel = targetPanel;
        CanvasGroup cg = targetPanel.GetComponent<CanvasGroup>();
        if (cg == null) cg = targetPanel.AddComponent<CanvasGroup>();
        InvokeRepeating("FadeOut", 0f, 0.02f);
    }

    public void Show(GameObject targetPanel)
    {
        CancelInvoke("FadeIn");
        targetPanel.SetActive(true);
        CanvasGroup cg = targetPanel.GetComponent<CanvasGroup>();
        if (cg == null) cg = targetPanel.AddComponent<CanvasGroup>();
        cg.alpha = 0f;
        currentFadeInPanel = targetPanel;
        InvokeRepeating("FadeIn", 0f, 0.02f);
    }

    public void GameOver()
    {
        // 카메라 추적 비활성화
        Forever_ChaseCameraH.instance.isEnabled = false;

        InvokeRepeating("MoveCameraUp", 0f, 0.02f);
        Hide(gamePanel);
        Show(gameOverPanel);
    }

    void MoveCameraDown()
    {
        Camera mainCamera = Camera.main;
        Vector3 pos = mainCamera.transform.position;

        if (pos.y > gamePosition)
        {
            pos.y -= 0.1f;
            mainCamera.transform.position = pos;
        }
        else
        {
            pos.y = gamePosition;
            mainCamera.transform.position = pos;
            CancelInvoke("MoveCameraDown");
            // base_pos를 새 위치로 업데이트
            Forever_ChaseCameraH.instance.UpdateBasePos(pos);

            // 카메라 이동 완료 후 추적 활성화
            Forever_ChaseCameraH.instance.isEnabled = true;
        }
    }

    void MoveCameraUp()
    {
        Camera mainCamera = Camera.main;
        Vector3 pos = mainCamera.transform.position;

        if (pos.y < titlePosition)
        {
            pos.y += 0.1f;
            mainCamera.transform.position = pos;
        }
        else
        {
            pos.y = titlePosition;
            mainCamera.transform.position = pos;
            CancelInvoke("MoveCameraUp");

            // 타이틀로 돌아가면 추적 비활성화 유지
            // (타이틀 화면에서는 추적 불필요)
        }
    }

    void FadeOut()
    {
        if (currentFadeOutPanel == null) return;
        CanvasGroup cg = currentFadeOutPanel.GetComponent<CanvasGroup>();
        if (cg == null) return;

        if (cg.alpha > 0.01f)
        {
            cg.alpha *= 0.9f;
        }
        else
        {
            cg.alpha = 0f;
            currentFadeOutPanel.SetActive(false);
            CancelInvoke("FadeOut");
            currentFadeOutPanel = null;
        }
    }

    void FadeIn()
    {
        if (currentFadeInPanel == null) return;
        CanvasGroup cg = currentFadeInPanel.GetComponent<CanvasGroup>();
        if (cg == null) return;

        if (cg.alpha < 0.99f)
        {
            cg.alpha += 0.05f;
        }
        else
        {
            cg.alpha = 1f;
            CancelInvoke("FadeIn");
            currentFadeInPanel = null;
        }
    }
}