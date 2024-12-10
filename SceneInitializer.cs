using UnityEngine;
using TMPro;

public class SceneInitializer : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // 当前场景的 TMP 文本

    private void Start()
    {
        if (ScoreManager.Instance != null && scoreText != null)
        {
            ScoreManager.Instance.SetScoreText(scoreText);
        }
    }
}
