using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // 单例模式
    private int totalScore = 0;          // 当前总分
    private TextMeshProUGUI scoreText;   // TMP 文本，用于显示分数

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 保证跨场景保留
        }
        else
        {
            Destroy(gameObject); // 防止重复实例
        }
    }

    /// <summary>
    /// 增加分数
    /// </summary>
    /// <param name="points">要增加的分数</param>
    public void AddScore(int points)
    {
        totalScore += points;
        UpdateScoreDisplay();
    }

    /// <summary>
    /// 获取总分
    /// </summary>
    /// <returns>当前总分</returns>
    public int GetTotalScore()
    {
        return totalScore;
    }

    /// <summary>
    /// 设置当前场景的 TMP 文本
    /// </summary>
    /// <param name="newScoreText">新的 TMP 文本</param>
    public void SetScoreText(TextMeshProUGUI newScoreText)
    {
        scoreText = newScoreText;
        UpdateScoreDisplay();
    }

    /// <summary>
    /// 更新分数显示
    /// </summary>
    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {totalScore}";
        }
    }

    /// <summary>
    /// 重置分数
    /// </summary>
    public void ResetScore()
    {
        totalScore = 0;
        UpdateScoreDisplay();
    }
}
