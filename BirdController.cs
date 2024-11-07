using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float speed = 2f;             // 鸟的移动速度
    public float leftBound = -8f;        // 左边界
    public float rightBound = 8f;        // 右边界
    private Vector2 moveDirection;       // 水平移动方向
    private bool isFacingRight = true;   // 当前的朝向

    void Start()
    {
        // 初始化鸟的水平移动方向（左或右）
        moveDirection = GetRandomHorizontalDirection();
    }

    void Update()
    {
        // 移动鸟并检测边界
        MoveBird();

        // 根据水平移动方向翻转鸟
        FlipBird();
    }

    void MoveBird()
    {
        // 计算新的水平位置
        Vector2 newPosition = (Vector2)transform.position + moveDirection * speed * Time.deltaTime;

        // 检测并处理边界反弹
        if (newPosition.x > rightBound || newPosition.x < leftBound)
        {
            moveDirection.x = -moveDirection.x; // 水平反弹
            newPosition.x = Mathf.Clamp(newPosition.x, leftBound, rightBound);
        }

        // 更新鸟的位置
        transform.position = newPosition;
    }

    void FlipBird()
    {
        // 检测当前的水平移动方向并翻转鸟
        if (moveDirection.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveDirection.x < 0 && isFacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        // 翻转鸟的X轴缩放以改变朝向
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    Vector2 GetRandomHorizontalDirection()
    {
        // 随机生成水平方向（左或右）
        float randomX = Random.Range(0, 2) == 0 ? -1f : 1f;
        return new Vector2(randomX, 0).normalized; // 仅在水平方向移动
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 检查是否与图钉碰撞
        if (other.CompareTag("Pin"))
        {
            // 作为障碍物，销毁图钉
            Destroy(other.gameObject);
        }
    }
}
