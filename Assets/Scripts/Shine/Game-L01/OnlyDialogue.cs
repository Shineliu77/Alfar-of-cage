using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlyDialogue : MonoBehaviour
{
    public string[] Dialogues;          // 對話內容
    private int i = 0;                  // 當前對話索引
    public Text DialogueText;           // 對話顯示文字元件
    public GameObject[] NextObjs;       // 需要啟用的多個物件陣列
    public GameObject Player;           // Player 物件 (用於找到 PlayerControl 腳本)
    private PlayerControl playerControl; // 引用 PlayerControl 腳本

    void Start()
    {
        // 確保 Player 不為 null 並取得 PlayerControl 腳本
        if (Player != null)
        {
            playerControl = Player.GetComponent<PlayerControl>();
            if (playerControl != null)
            {
                playerControl.enabled = false; // 遊戲開始時禁用 PlayerControl
            }
        }

        if (Dialogues.Length > 0)
        {
            DialogueText.text = Dialogues[0]; // 初始化第一句對話
        }
    }

    public void ClickNext()
    {
        i++;
        if (i >= Dialogues.Length)
        {
            gameObject.SetActive(false);

            // 一次啟用陣列中的所有物件
            foreach (GameObject obj in NextObjs)
            {
                if (obj != null)
                {
                    obj.SetActive(true); // 啟用每個設置的物件
                }
            }

            // 對話結束時啟用 PlayerControl 腳本
            if (playerControl != null)
            {
                playerControl.enabled = true;
            }

            return; // 避免執行後續程式碼
        }

        i = Mathf.Clamp(i, 0, Dialogues.Length - 1); // 確保索引在合法範圍內
        DialogueText.text = Dialogues[i];           // 更新顯示文字
    }
}
