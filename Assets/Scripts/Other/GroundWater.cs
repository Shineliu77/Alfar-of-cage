using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundWater : MonoBehaviour
{
    public bool playerInRange; // 玩家是否在範圍內
    public bool isActive = true; // GroundWater 是否啟用

    [Header("死亡設定")]
    public float deathDelay = 0f; // 接觸後延遲幾秒死亡（如果需要）

    void Update()
    {
        // 如果玩家在範圍內並且 GroundWater 是啟用狀態
        if (playerInRange && isActive)
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        // 查找玩家腳本並調用死亡方法
        PlayerControl player = GameObject.FindObjectOfType<PlayerControl>();
        if (player != null)
        {
            isActive = false; // 禁用 GroundWater（防止多次觸發）
            player.Die(); // 觸發玩家死亡邏輯
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 檢測玩家是否進入範圍
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 檢測玩家是否離開範圍
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
