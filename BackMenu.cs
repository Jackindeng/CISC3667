using UnityEngine;
using UnityEngine.SceneManagement;

public class Backmenu : MonoBehaviour
{
    // 返回主菜单的方法
    public void BackToMainMenu()
    {
        // 确保游戏时间恢复正常（如果有暂停功能）
        Time.timeScale = 1;

        // 加载主菜单场景
        SceneManager.LoadScene("MainMenu");
    }
}
