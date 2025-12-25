using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("실시간 UI")]
    public TextMeshProUGUI scoreText;

    [Header("결과창 UI")]
    public GameObject resultPanel;
    public TextMeshProUGUI finalScoreText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        resultPanel.SetActive(false);
    }

    public void UpdateScoreUI(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void ShowResult(int finalScore)
    {
        finalScoreText.text = finalScore.ToString();
        resultPanel.SetActive(true);
    }
}