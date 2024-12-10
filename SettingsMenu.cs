using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    [Header("UI Components")]
    public Slider volumeSlider;           // 音量滑块
    public TMP_Dropdown difficultyDropdown; // 难度选择下拉框
    public Toggle musicToggle;            // 背景音乐开关
    public Button returnToMainMenuButton; // 返回主菜单按钮

    [Header("Audio")]
    public AudioSource backgroundMusic; // 背景音乐音频源

    [Header("Difficulty Settings")]
    public DifficultySettings[] difficultySettings; // 不同难度的配置
    public static DifficultySettings CurrentDifficulty; // 当前难度设置

    private void Start()
    {

        InitializeSettings(); // 初始化设置

        // 绑定事件监听
        volumeSlider.onValueChanged.AddListener(SetVolume);
        difficultyDropdown.onValueChanged.AddListener(SetDifficulty);
        musicToggle.onValueChanged.AddListener(ToggleMusic);
        returnToMainMenuButton.onClick.AddListener(ReturnToMainMenu);
    }



    /// <summary>
    /// 初始化设置
    /// </summary>
    private void InitializeSettings()
    {
        // 初始化音量
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
        volumeSlider.value = savedVolume;
        AudioListener.volume = savedVolume;

        // 初始化难度
        int savedDifficulty = PlayerPrefs.GetInt("Difficulty", 1);
        difficultyDropdown.value = savedDifficulty;
        ApplyDifficulty(savedDifficulty);

        // 初始化背景音乐开关
        bool isMusicOn = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        musicToggle.isOn = isMusicOn;
        if (backgroundMusic != null)
        {
            backgroundMusic.mute = !isMusicOn;
        }
    }

    /// <summary>
    /// 设置音量
    /// </summary>
    /// <param name="volume">音量值</param>
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
        Debug.Log($"Volume set to: {volume}");
    }

    /// <summary>
    /// 设置游戏难度
    /// </summary>
    /// <param name="difficultyIndex">难度索引</param>
    public void SetDifficulty(int difficultyIndex)
    {
        PlayerPrefs.SetInt("Difficulty", difficultyIndex);
        ApplyDifficulty(difficultyIndex);
        Debug.Log($"Difficulty set to: {difficultySettings[difficultyIndex].name}");
    }

    /// <summary>
    /// 应用游戏难度
    /// </summary>
    /// <param name="difficultyIndex">难度索引</param>
    private void ApplyDifficulty(int difficultyIndex)
    {
        if (difficultyIndex >= 0 && difficultyIndex < difficultySettings.Length)
        {
            CurrentDifficulty = difficultySettings[difficultyIndex];
            Debug.Log($"Applied difficulty: {CurrentDifficulty.name}");
        }
    }

    /// <summary>
    /// 切换背景音乐
    /// </summary>
    /// <param name="isEnabled">背景音乐是否开启</param>
    public void ToggleMusic(bool isEnabled)
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.mute = !isEnabled;
        }
        PlayerPrefs.SetInt("MusicEnabled", isEnabled ? 1 : 0);
        Debug.Log($"Music enabled: {isEnabled}");
    }

    /// <summary>
    /// 返回主菜单
    /// </summary>
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // 替换为你的主菜单场景名称
    }
}
