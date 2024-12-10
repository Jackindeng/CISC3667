using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // 角色移动速度
    public Vector2 minBounds; // 背景的最小范围
    public Vector2 maxBounds; // 背景的最大范围

    private Vector3 movement; // 临时存储移动向量

    void Update()
    {
        // 获取玩家输入
        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        // 计算新的位置
        movement = transform.position + new Vector3(moveX, moveY, 0);

        // 限制角色在背景范围内
        movement.x = Mathf.Clamp(movement.x, minBounds.x, maxBounds.x);
        movement.y = Mathf.Clamp(movement.y, minBounds.y, maxBounds.y);

        // 更新角色位置
        transform.position = movement;
    }

    // 可视化边界范围（仅在编辑模式下显示）
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(minBounds.x, minBounds.y, 0), new Vector3(minBounds.x, maxBounds.y, 0));
        Gizmos.DrawLine(new Vector3(maxBounds.x, minBounds.y, 0), new Vector3(maxBounds.x, maxBounds.y, 0));
        Gizmos.DrawLine(new Vector3(minBounds.x, minBounds.y, 0), new Vector3(maxBounds.x, minBounds.y, 0));
        Gizmos.DrawLine(new Vector3(minBounds.x, maxBounds.y, 0), new Vector3(maxBounds.x, maxBounds.y, 0));
    }
}
