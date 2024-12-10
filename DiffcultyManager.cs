using UnityEngine;

[System.Serializable]
public class DifficultySettings
{
    public string name;       // 难度名称（如 Easy, Medium, Hard）
    public float moveSpeed;   // 气球移动速度
    public float maxSize;     // 气球的最大尺寸
    public float scoreMultiplier; // 分数倍率
}
