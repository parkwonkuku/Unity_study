using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // 어디서든 접근 가능하게 싱글톤 설정

    public int score = 0;
    public bool isGameOver = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // 코인을 먹었을 때 호출할 함수
    public void AddScore(int amount)
    {
        if (isGameOver) return;
        score += amount;
        UIManager.Instance.UpdateScoreUI(score);
    }

    // 장애물에 부딪혔을 때 호출할 함수
    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0; // 게임 일시정지
        UIManager.Instance.ShowResult(score);
    }

    // 다시 시작 버튼 등에 연결할 함수
    public void OnRestartClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1; // 시간 재개
    }
}