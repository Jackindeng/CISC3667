using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance; // 单例模式，方便全局调用

    private void Awake()
    {
        // 保证单例模式的唯一性
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 确保切换场景后不会销毁
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 延迟进入下一关
    /// </summary>
    /// <param name="delay">延迟时间（秒）</param>
    public void LoadNextLevelWithDelay(float delay)
    {
        StartCoroutine(DelayedLoadNextLevel(delay));
    }

    /// <summary>
    /// 延迟加载下一关的协程
    /// </summary>
    /// <param name="delay">延迟时间（秒）</param>
    private IEnumerator DelayedLoadNextLevel(float delay)
    {
        Debug.Log($"Delaying next level by {delay} seconds...");
        yield return new WaitForSeconds(delay);

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log($"Loading next level with index: {nextIndex}");
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.LogWarning("No more levels to load. Returning to main menu or restarting.");
            SceneManager.LoadScene(0); // 假设主菜单是第一个场景
        }
    }
}
