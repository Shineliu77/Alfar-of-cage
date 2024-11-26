using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private Collider2D platformCollider;

    void Start()
    {
        // 取得地板的 Collider
        platformCollider = GetComponent<Collider2D>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("player")) // 確認碰撞物是玩家
        {
            // 取得玩家的 Rigidbody 速度
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null && rb.velocity.y > 0) // 只有在往上的時候
            {
                Physics2D.IgnoreCollision(other, platformCollider, true); // 忽略碰撞
            }
            else
            {
                Physics2D.IgnoreCollision(other, platformCollider, false); // 開啟碰撞
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            // 確保離開時恢復碰撞
            Physics2D.IgnoreCollision(other, platformCollider, false);
        }
    }
}
