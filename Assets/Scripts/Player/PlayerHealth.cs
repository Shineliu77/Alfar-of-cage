using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth = 100;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // 玩家受到傷害
    public void TakeDamage(int damage, Vector2 knockback)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            Debug.Log("Player Died");
            // 玩家死亡邏輯
        }

        // 對玩家施加擊退效果
        ApplyKnockback(knockback);
    }

    // 擊退效果
    private void ApplyKnockback(Vector2 knockback)
    {
        rb.AddForce(knockback, ForceMode2D.Impulse); // 對玩家施加擊退
    }
}
