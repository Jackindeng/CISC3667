using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("UI Components")]
    public Slider volumeSlider;         // 音量控制滑块
    public Dropdown difficultyDropdown; // 难度选择下拉菜单
    public Toggle musicToggle;          // 背景音乐开关

    private AudioSource bgMusicSource;  // 背景音乐的AudioSource

    void Start()
    {
        // 初始化音量滑块
        volumeSlider.value = AudioListener.volume; // 将滑块值设为当前的全局音量
        volumeSlider.onValueChanged.AddListener(SetVolume); // 监听音量滑块的值变化

        // 初始化难度下拉菜单
        difficultyDropdown.onValueChanged.AddListener(SetDifficulty);
        difficultyDropdown.value = PlayerPrefs.GetInt("Difficulty", 1); // 加载保存的难度（默认为中等）

        // 初始化背景音乐开关
        musicToggle.onValueChanged.AddListener(ToggleMusic);
        bgMusicSource = Object.FindFirstObjectByType<AudioSource>(); // 查找场景中的AudioSource组件
        if (bgMusicSource != null)
        {
            musicToggle.isOn = !bgMusicSource.mute; // 设置开关状态为当前背景音乐的静音状态
        }
    }

    // 设置全局音量
    public void SetVolume(float value)
    {
        AudioListener.volume = value; // 设置全局音量
    }

    // 设置游戏难度
    public void SetDifficulty(int index)
    {
        PlayerPrefs.SetInt("Difficulty", index); // 保存选择的难度
        Debug.Log("Difficulty set to: " + index);

        // 使用此难度值控制游戏难度（可根据游戏需求调整）
        switch (index)
        {
            case 0:
                // 简单难度设置，例如减少敌人数量或降低速度
                break;
            case 1:
                // 中等难度
                break;
            case 2:
                // 困难难度设置，例如增加敌人数量或提高速度
                break;
        }
    }

    // 控制背景音乐的开关
    public void ToggleMusic(bool isOn)
    {
        if (bgMusicSource != null)
        {
            bgMusicSource.mute = !isOn; // 根据开关状态设置背景音乐的静音
        }
    }
}
