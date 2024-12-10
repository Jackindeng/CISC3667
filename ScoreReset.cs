using UnityEngine;

public class MainMenuInitializer : MonoBehaviour
{
    private void Start()
    {
        // 确保分数在主菜单初始化时被重置
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetScore();
        }
    }
}
