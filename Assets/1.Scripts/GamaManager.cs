using UnityEngine;
using UnityEngine.SceneManagement;

// GameManager는 프로젝트 전반에 하나만 존재해야 하므로 싱글톤 패턴을 적용합니다.
public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // 전역 접근용 인스턴스

    private void Awake()
    {
        // 이미 인스턴스가 있으면 중복 제거
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 최초 생성 시 인스턴스로 지정하고 파괴되지 않도록 설정
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // 메인 메뉴에서 게임 시작 시 호출할 메소드
    public void StartGame()
    {
        // 페이드 아웃 후 Gameplay 씬 로드
        SceneTransitionManager stm = FindObjectOfType<SceneTransitionManager>();
        if (stm != null)
            stm.LoadScene("Gameplay");
    }

    // 게임 오버 시 호출할 메소드
    public void GameOver()
    {
        // 예: 보상 씬으로 이동
        SceneTransitionManager stm = FindObjectOfType<SceneTransitionManager>();
        if (stm != null)
            stm.LoadScene("Reward");
    }

    // 전역에서 씬 전환이 필요할 때 호출
    public void ChangeScene(string sceneName)
    {
        SceneTransitionManager stm = FindObjectOfType<SceneTransitionManager>();
        if (stm != null)
            stm.LoadScene(sceneName);
    }
}
