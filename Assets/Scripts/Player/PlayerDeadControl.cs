using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            Die();
        }
    }
    public void Die()
    {
        // 玩家死亡處理邏輯
        Debug.Log("Player Died");

        // 停止遊戲：將時間設置為 0（暫停遊戲）
        Time.timeScale = 0;

        // 停止玩家的操作：禁用物理和碰撞，保持玩家物件存在但不可見
        SpriteRenderer[] renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
        Collider2D[] colliders = gameObject.GetComponentsInChildren<Collider2D>();
        Rigidbody2D[] rigidbodies = gameObject.GetComponentsInChildren<Rigidbody2D>();

        // 禁用渲染、碰撞和物理
        foreach (var renderer in renderers)
        {
            renderer.enabled = false;
        }
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = true;
        }

        // 顯示 Game Over 畫面
        GameObject.FindObjectOfType<GameOverManager>().ShowGameOver();
    }
}
