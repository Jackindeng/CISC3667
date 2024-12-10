using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 2f; // 子弹存活时间

    void Start()
    {
        // 定时销毁子弹
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 检测碰撞并销毁子弹
        Destroy(gameObject);
    }
}
