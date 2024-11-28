using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerDialogue : MonoBehaviour
{
    public string[] Dialogues;          // 對話內容
    private int i = 0;                  // 當前對話索引
    public Text DialogueText;           // 對話顯示文字元件
    public GameObject[] ObjectsToDisable; // 需要禁用的物件陣列
    public GameObject Player;           // Player 物件 (用於找到 PlayerControl 腳本)
    private PlayerControl playerControl; // 引用 PlayerControl 腳本
    public GameObject TriggerObject;    // 觸發對話的物件

    private bool isDialogueActive = false; // 判斷對話是否正在進行

    void Start()
    {
        Debug.Log("fku");
        // 確保 Player 不為 null 並取得 PlayerControl 腳本
        if (Player != null)
        {
            playerControl = Player.GetComponent<PlayerControl>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called. Collided with: " + other.gameObject.name);
        if (other.gameObject == TriggerObject && !isDialogueActive)
        {
            Debug.Log("TriggerObject matched, starting dialogue...");
            StartDialogue();
        }
    }



    void StartDialogue()
    {
        isDialogueActive = true;

        // 禁用 PlayerControl
        if (playerControl != null)
        {
            playerControl.enabled = false;
        }

        // 禁用陣列中的所有物件
        foreach (GameObject obj in ObjectsToDisable)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }

        // 初始化第一句對話
        if (Dialogues.Length > 0)
        {
            DialogueText.text = Dialogues[0];
        }
    }

    public void ClickNext()
    {
        i++;
        if (i >= Dialogues.Length)
        {
            EndDialogue();
            return; // 避免執行後續程式碼
        }

        i = Mathf.Clamp(i, 0, Dialogues.Length - 1); // 確保索引在合法範圍內
        DialogueText.text = Dialogues[i];           // 更新顯示文字
    }

    void EndDialogue()
    {
        isDialogueActive = false;

        // 啟用 PlayerControl
        if (playerControl != null)
        {
            playerControl.enabled = true;
        }

        // 啟用陣列中的所有物件
        foreach (GameObject obj in ObjectsToDisable)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }

        // 清空對話文字
        DialogueText.text = "";

        // 重置對話索引
        i = 0;
    }
}
