using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100;  // 最大血量
    private int currentHealth;    // 當前血量

    void Start()
    {
        // 初始化血量為最大血量
        currentHealth = maxHealth;
    }

    // 讓 BOSS 受到傷害的函數
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 當 BOSS 死亡時的邏輯
    void Die()
    {
        Debug.Log("BOSS is dead!");
        // 在這裡可以加入死亡動畫、音效或銷毀物件等邏輯
        Destroy(gameObject); // 銷毀 BOSS 物件
    }

    // 取得當前血量
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    // 取得最大血量
    public int GetMaxHealth()
    {
        return maxHealth;
    }
}

