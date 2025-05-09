using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel; // 參考 Game Over 的 Panel
    public float delayBeforeGameOver = 0.5f; // 延遲時間

    [Header("UI 控制")]
    public GameObject[] uiElementsToDisable; // 要關閉的 UI 元素（例如血條、控制按鈕等）

    void Start()
    {
        // 隱藏 Game Over 面板
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        // 隱藏遊戲 UI（例如血條和控制介面）
        foreach (var uiElement in uiElementsToDisable)
        {
            uiElement.SetActive(false);
        }

      
        gameOverPanel.SetActive(true);
    }

    private void DisplayGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void RespawnPlayer()
    {
        // 根據標籤找到玩家物件
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            // 重新啟用玩家物件的渲染、碰撞和物理
            SpriteRenderer[] renderers = playerObject.GetComponentsInChildren<SpriteRenderer>();
            Collider2D[] colliders = playerObject.GetComponentsInChildren<Collider2D>();
            Rigidbody2D[] rigidbodies = playerObject.GetComponentsInChildren<Rigidbody2D>();

            // 啟用渲染、碰撞和物理
            foreach (var renderer in renderers)
            {
                renderer.enabled = true;
            }
            foreach (var collider in colliders)
            {
                collider.enabled = true;
            }
            foreach (var rb in rigidbodies)
            {
                rb.isKinematic = false;
            }

            // 設置玩家位置為最後對話位置
            playerObject.transform.position = TriggerDialogue.lastDialoguePosition;

            // 確保玩家的血量回復
            PlayerHealth playerHealth = playerObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.playerHealth = playerHealth.maxHealth;  // 重置血量

                // 更新血條
                if (playerHealth.healthSlider != null)
                {
                    playerHealth.healthSlider.value = playerHealth.playerHealth;
                }
            }

            Debug.Log("Player respawned via GameOverManager.");
        }
        else
        {
            Debug.LogError("Player object not found in the scene.");
        }

        // 隱藏 Game Over 畫面
        gameOverPanel.SetActive(false);

        // 恢復 UI 元素
        foreach (var uiElement in uiElementsToDisable)
        {
            uiElement.SetActive(true);
        }

        // 恢復遊戲：將時間恢復正常（設置為 1）
        Time.timeScale = 1;
    }
    public void BackToMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu");
    }






}
