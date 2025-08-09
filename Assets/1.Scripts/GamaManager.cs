using UnityEngine;
using UnityEngine.SceneManagement;

// GameManager�� ������Ʈ ���ݿ� �ϳ��� �����ؾ� �ϹǷ� �̱��� ������ �����մϴ�.
public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // ���� ���ٿ� �ν��Ͻ�

    private void Awake()
    {
        // �̹� �ν��Ͻ��� ������ �ߺ� ����
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // ���� ���� �� �ν��Ͻ��� �����ϰ� �ı����� �ʵ��� ����
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // ���� �޴����� ���� ���� �� ȣ���� �޼ҵ�
    public void StartGame()
    {
        // ���̵� �ƿ� �� Gameplay �� �ε�
        SceneTransitionManager stm = FindObjectOfType<SceneTransitionManager>();
        if (stm != null)
            stm.LoadScene("Gameplay");
    }

    // ���� ���� �� ȣ���� �޼ҵ�
    public void GameOver()
    {
        // ��: ���� ������ �̵�
        SceneTransitionManager stm = FindObjectOfType<SceneTransitionManager>();
        if (stm != null)
            stm.LoadScene("Reward");
    }

    // �������� �� ��ȯ�� �ʿ��� �� ȣ��
    public void ChangeScene(string sceneName)
    {
        SceneTransitionManager stm = FindObjectOfType<SceneTransitionManager>();
        if (stm != null)
            stm.LoadScene(sceneName);
    }
}
