using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance; // 单例模式

    private int[] highScores = new int[5]; // 存储 5 个最高分

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 保留跨场景对象
            LoadHighScores();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 更新最高分记录
    /// </summary>
    /// <param name="newScore">新的分数</param>
    public void UpdateHighScores(int newScore)
    {
        for (int i = 0; i < highScores.Length; i++)
        {
            if (newScore > highScores[i])
            {
                for (int j = highScores.Length - 1; j > i; j--)
                {
                    highScores[j] = highScores[j - 1];
                }
                highScores[i] = newScore;
                SaveHighScores();
                break;
            }
        }
    }

    /// <summary>
    /// 获取最高分数组
    /// </summary>
    /// <returns>最高分数组</returns>
    public int[] GetHighScores()
    {
        return highScores;
    }

    /// <summary>
    /// 保存最高分记录
    /// </summary>
    private void SaveHighScores()
    {
        for (int i = 0; i < highScores.Length; i++)
        {
            PlayerPrefs.SetInt($"HighScore_{i}", highScores[i]);
        }
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 加载最高分记录
    /// </summary>
    private void LoadHighScores()
    {
        for (int i = 0; i < highScores.Length; i++)
        {
            highScores[i] = PlayerPrefs.GetInt($"HighScore_{i}", 0);
        }
    }
}
