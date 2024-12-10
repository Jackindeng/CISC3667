using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab; // 子弹预制件
    public Transform firePoint; // 发射点位置
    public float projectileSpeed = 10f; // 子弹速度
    public float fireRate = 0.5f; // 射击间隔时间

    private float nextFireTime = 0f; // 下次允许射击的时间

    void Update()
    {
        // 检测射击输入（鼠标左键或空格键）
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // 创建子弹实例
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // 设置子弹的速度
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.up * projectileSpeed;
        }
    }
}
