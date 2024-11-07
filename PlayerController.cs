using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject pinPrefab;     // 图钉预制体
    public Transform pinSpawnPoint;  // 图钉生成位置
    public float speed = 5f;         // 控制移动速度
    public float minX = -8f;         // 背景左边界
    public float maxX = 8f;          // 背景右边界
    public float minY = -4f;         // 背景下边界
    public float maxY = 4f;          // 背景上边界

    private Rigidbody2D rb;
    private bool isFacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovement();

        // 检查Fire1输入（默认为Ctrl键）
        if (Input.GetButtonDown("Fire1"))
        {
            ShootPin();
        }
    }

    void HandleMovement()
    {
        // 获取水平和垂直输入
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 计算新的位置
        Vector2 newPosition = rb.position + new Vector2(horizontalInput * speed * Time.deltaTime, verticalInput * speed * Time.deltaTime);

        // 限制角色在背景范围内移动
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // 更新角色位置
        rb.MovePosition(newPosition);

        // 翻转角色以朝向移动方向
        if (horizontalInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && isFacingRight)
        {
            Flip();
        }
    }

    void ShootPin()
    {
        Vector3 spawnPosition = pinSpawnPoint != null ? pinSpawnPoint.position : transform.position;

        // 在玩家当前位置或指定的生成点位置实例化图钉
        GameObject pin = Instantiate(pinPrefab, spawnPosition, Quaternion.identity);

        // 启用图钉的移动脚本
        PinMovement pinMovement = pin.GetComponent<PinMovement>();
        if (pinMovement != null)
        {
            pinMovement.enabled = true;
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
