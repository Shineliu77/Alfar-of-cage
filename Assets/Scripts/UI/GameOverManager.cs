using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

        // 延遲顯示 Game Over 畫面
        Invoke("DisplayGameOver", delayBeforeGameOver);
    }

    private void DisplayGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        // 重新載入當前場景
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        // 載入主菜單場景（確保主菜單場景名稱正確）
        SceneManager.LoadScene("Menu");
    }
}
