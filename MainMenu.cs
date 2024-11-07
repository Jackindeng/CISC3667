using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("level 1"); // 替换为您的游戏场景名称
    }

    public void OpenInstructions()
    {
       SceneManager.LoadScene("InstructionsMenu"); // 显示游戏说明，例如弹出说明面板或跳转到说明场景
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsMenu"); // 跳转到设置菜单场景
    }
}
