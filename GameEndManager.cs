using UnityEngine;
using TMPro;

public class GameEndManager : MonoBehaviour
{
    public TextMeshProUGUI totalScoreText; // 用于显示总分的 TMP 文本
    public TextMeshProUGUI highScoreText;  // 用于显示最高分的 TMP 文本

    private void Start()
    {
        int totalScore = ScoreManager.Instance.GetTotalScore();

        // 显示总分
        if (totalScoreText != null)
        {
            totalScoreText.text = $"Total Score: {totalScore}";
        }

        // 更新最高分
        HighScoreManager.Instance.UpdateHighScores(totalScore);

        // 显示最高分
        if (highScoreText != null)
        {
            int[] highScores = HighScoreManager.Instance.GetHighScores();
            highScoreText.text = "High Scores:\n";
            for (int i = 0; i < highScores.Length; i++)
            {
                highScoreText.text += $"{i + 1}: {highScores[i]}\n";
            }
        }
    }
}
