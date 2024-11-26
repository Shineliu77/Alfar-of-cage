using UnityEngine;

public class HailDamage : MonoBehaviour
{
    private float damage;

    public void SetDamage(float damageAmount)
    {
        damage = damageAmount;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 檢查是否碰撞到 BOSS
        if (collision.gameObject.CompareTag("Boss"))
        {
            // 讓 BOSS 受傷，並將 damage 強制轉換為 int
            BossHealth bossHealth = collision.gameObject.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage((int)damage); // 將 damage 轉換為 int
            }

            // 撞擊後銷毀冰雹
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            // 當冰雹碰到地面時銷毀冰雹
            Destroy(gameObject);
        }
    }
}
