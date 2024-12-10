using UnityEngine;
using UnityEngine.SceneManagement;

public class Balloon : MonoBehaviour
{
    public GameObject explosionEffect; // 粒子系统预制件
    public AudioClip explosionSound; // 音效文件
    public Vector2 moveBoundsMin;
    public Vector2 moveBoundsMax;
    public float moveSpeed = 2f;
    public float growthRate = 0.1f;
    public float maxSize = 3f;
    public float scoreMultiplier = 10f;

    private Vector3 moveDirection;
    private float spawnTime;

    void Start()
    {
         // 初始化气球参数，根据当前难度调整
        if (SettingsManager.CurrentDifficulty != null)
        {
            moveSpeed = SettingsManager.CurrentDifficulty.moveSpeed;
            maxSize = SettingsManager.CurrentDifficulty.maxSize;
            scoreMultiplier = SettingsManager.CurrentDifficulty.scoreMultiplier;
        }

        moveDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
        spawnTime = Time.time;
    }

    void Update()
    {
        Move();
        transform.localScale += Vector3.one * growthRate * Time.deltaTime;

        if (transform.localScale.x >= maxSize)
        {
            RestartLevel();
        }
    }

    void Move()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if (transform.position.x <= moveBoundsMin.x || transform.position.x >= moveBoundsMax.x)
        {
            moveDirection.x = -moveDirection.x;
        }

        if (transform.position.y <= moveBoundsMin.y || transform.position.y >= moveBoundsMax.y)
        {
            moveDirection.y = -moveDirection.y;
        }

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, moveBoundsMin.x, moveBoundsMax.x),
            Mathf.Clamp(transform.position.y, moveBoundsMin.y, moveBoundsMax.y),
            transform.position.z
        );
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {

	Debug.Log("Hit by Pin");
            // 播放粒子特效
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

            // 播放音效
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);

            // 计算分数
            float existTime = Time.time - spawnTime;
            int score = Mathf.Max(1, Mathf.FloorToInt(scoreMultiplier / existTime));
            ScoreManager.Instance.AddScore(score);

            // 销毁子弹和气球
            Destroy(collision.gameObject);
            Destroy(gameObject);

            // 进入下一关

            LevelManager.Instance.LoadNextLevelWithDelay(2f);
        }
    }

 }