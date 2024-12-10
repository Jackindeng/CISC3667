using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false; // 游戏是否暂停
    public GameObject pauseMenuUI; // 暂停菜单的 UI 对象

    void Update()
    {
        // 按下 Esc 或 P 键切换暂停状态
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }


 public void TogglePauseMenu()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    /// <summary>
    /// 暂停游戏
    /// </summary>
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // 暂停游戏
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true); // 显示暂停菜单
        }
    }

    /// <summary>
    /// 恢复游戏
    /// </summary>
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // 恢复游戏
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false); // 隐藏暂停菜单
        }
    }

    /// <summary>
    /// 返回主菜单（示例逻辑，需根据实际项目调整）
    /// </summary>
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // 恢复正常时间速度
        Debug.Log("Returning to main menu...");
        // 在这里加载主菜单场景
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit(); // 退出游戏
    }
}
