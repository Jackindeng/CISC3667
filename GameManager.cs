using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;       // 单例模式实例
    public TextMeshProUGUI scoreText;         // 用于显示分数的TextMeshPro UI组件
    public GameObject pauseMenu;              // 引用暂停菜单的面板
       public GameObject canvas;  

    private int score = 0;                    // 当前得分
    private bool isPaused = false;            // 游戏是否暂停的标志

    void Awake()
    {
        // 确保只有一个GameManager实例（单例模式）
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 保持GameManager在场景切换时不被销毁
        }
        else
        {
            Destroy(gameObject);
	  
        } 
		   }

    void Update()
    {
        // 按下 "Escape" 键切换暂停和恢复
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    // 增加分数的方法
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText(); // 更新分数显示
    }

    // 更新分数显示的UI文本
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    // 进入下一关的方法
    public void NextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        
        // 检查是否还有下一关
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex); // 加载下一个场景
        }
        else
        {
            Debug.Log("Game Completed! Returning to Main Menu.");
            SceneManager.LoadScene("MainMenu"); // 所有关卡完成后返回主菜单
        }
    }

    // 重新启动当前关卡
    public void RestartLevel()
    {
        Time.timeScale = 1; // 确保时间恢复正常
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 重新加载当前关卡
    }

    // 返回主菜单
    public void BackToMainMenu()
    {
        Time.timeScale = 1; // 确保时间恢复正常
        SceneManager.LoadScene("MainMenu"); // 加载主菜单场景
    }

    // 暂停游戏
    public void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true);  // 显示暂停菜单
        Time.timeScale = 0;         // 将游戏时间设置为0，暂停所有动作
    }

    // 恢复游戏
    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false); // 隐藏暂停菜单
        Time.timeScale = 1;         // 将游戏时间恢复到正常速度
    }
}
