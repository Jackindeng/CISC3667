using UnityEngine;

public class PinMovement : MonoBehaviour
{
    public float speed = 10f;                 // Pin的垂直移动速度
    public int scorePerBalloon = 100;         // 每次击中气球获得的分数
    public AudioClip popSound;                // 气球爆炸音效

    private GameManager gameManager;
    private AudioSource audioSource;

    void Start()
    {
        // 获取GameManager的引用
        gameManager = GameManager.Instance;

        // 添加AudioSource组件，用于播放音效
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        // 垂直移动Pin
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        // 离开屏幕时销毁Pin
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查是否与气球发生碰撞
        if (collision.CompareTag("Balloon"))
        {
            // 更新分数
            if (gameManager != null)
            {
                gameManager.AddScore(scorePerBalloon);
            }

            // 播放气球爆炸音效
            if (popSound != null)
            {
                audioSource.PlayOneShot(popSound);
            }

            // 销毁气球和Pin
            Destroy(collision.gameObject); // 销毁气球
            Destroy(gameObject, popSound.length); // 等待音效播放完再销毁自己

            // 进入下一关
            if (gameManager != null)
            {
                gameManager.NextLevel();
            }
        }
    }
}
