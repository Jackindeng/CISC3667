using UnityEngine;

public class BalloonController : MonoBehaviour
{
    [Header("Growth Settings")]
    public float growRate = 0.1f;      // 气球每次增大的比例
    public float maxSize = 2.0f;       // 气球的最大尺寸

    [Header("Movement Settings")]
    public float moveSpeed = 2f;       // 气球移动速度
    public Vector2 minBounds;          // 移动范围的最小边界
    public Vector2 maxBounds;          // 移动范围的最大边界
    private Vector2 moveDirection;     // 移动方向
    private bool isGrowing = true;     // 气球是否继续增长

    void Start()
    {
        // 设置初始的随机移动方向
        moveDirection = GetRandomDirection();

        // 开始逐渐增大气球
        InvokeRepeating("IncreaseSize", 1f, 1f);

        // 每隔一段时间改变随机方向
        InvokeRepeating("ChangeDirection", 2f, 2f);
    }

    void Update()
    {
        // 移动气球并检测边界
        MoveBalloon();
    }

    void IncreaseSize()
    {
        // 如果气球未达到最大尺寸，继续增大
        if (transform.localScale.x < maxSize && isGrowing)
        {
            transform.localScale += new Vector3(growRate, growRate, 0);
        }
        else if (isGrowing)
        {
            // 达到最大尺寸，停止增长并爆炸
            isGrowing = false;
            Explode();
        }
    }

    void MoveBalloon()
    {
        // 计算新的位置
        Vector2 newPosition = (Vector2)transform.position + moveDirection * moveSpeed * Time.deltaTime;

        // 检测边界并反弹
        if (newPosition.x > maxBounds.x || newPosition.x < minBounds.x)
        {
            moveDirection.x = -moveDirection.x; // 水平反弹
            newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
        }
        if (newPosition.y > maxBounds.y || newPosition.y < minBounds.y)
        {
            moveDirection.y = -moveDirection.y; // 垂直反弹
            newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);
        }

        // 更新气球的位置
        transform.position = newPosition;
    }

    void ChangeDirection()
    {
        // 改变移动方向，获得一个新的随机方向
        moveDirection = GetRandomDirection();
    }

    void Explode()
    {
        // 实现气球爆炸效果，可以播放音效和特效
        Debug.Log("Balloon reached max size and exploded!");
        
        // 调用GameManager重新开始关卡
        GameManager.Instance.RestartLevel();
        
        // 销毁气球
        Destroy(gameObject);
    }

        int CalculateScore()
    {
        // 根据气球大小计算分数，气球越小分数越高
        float sizeFactor = maxSize / transform.localScale.x;
        return Mathf.RoundToInt(10 * sizeFactor);
    }

    Vector2 GetRandomDirection()
    {
        // 生成一个随机方向向量
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        return new Vector2(randomX, randomY).normalized;
    }
}
