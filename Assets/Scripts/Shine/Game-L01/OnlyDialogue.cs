using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlyDialogue : MonoBehaviour
{
    public string[] Dialogues;          // 對話內容
    private int i = 0;                  // 當前對話索引
    public Text DialogueText;           // 用於顯示對話文字的 UI Text 元件

    public GameObject[] NextObjs;       // 需要啟用的多個物件陣列
    public GameObject Player;           // Player 物件 (用來取得 PlayerControl 腳本)
    private PlayerControl playerControl; // 引用 PlayerControl 腳本
    public Image CharacterImage;        // 用於顯示角色立繪的 UI Image 元件
    public Image TAGImage;
    public Sprite[] CharacterSprites;   // 存儲每個對話對應的角色立繪圖片
    public Sprite[] TAGSprites;

    // 新增：角色名稱顯示的 Text 元件
    public Text CharacterNameText;      // 用於顯示角色名稱的 UI Text 元件
    public string[] CharacterNames;     // 存儲每個對話對應的角色名稱

    void Start()
    {
        // 確保 Player 物件不為 null，並取得 PlayerControl 腳本
        if (Player != null)
        {
            playerControl = Player.GetComponent<PlayerControl>();
            if (playerControl != null)
            {
                playerControl.enabled = false; // 遊戲開始時禁用 PlayerControl
            }
        }

        // 暫停遊戲時間
        Time.timeScale = 0f;

        if (Dialogues.Length > 0)
        {
            DialogueText.text = Dialogues[0]; // 顯示第一句對話
            UpdateCharacterImage(0); // 顯示對應的角色立繪
            UpdateTAGImage(0);
            UpdateCharacterName(0); // 顯示對應的角色名稱
        }
    }

    public void ClickNext()
    {
        i++; // 前進到下一句對話
        if (i >= Dialogues.Length)
        {
            gameObject.SetActive(false); // 隱藏對話框

            // 啟用陣列中的所有物件
            foreach (GameObject obj in NextObjs)
            {
                if (obj != null)
                {
                    obj.SetActive(true); // 啟用每個物件
                }
            }

            // 對話結束後，恢復遊戲時間
            Time.timeScale = 1f; // 恢復遊戲進行

            // 對話結束後，重新啟用 PlayerControl
            if (playerControl != null)
            {
                playerControl.enabled = true;
            }

            return; // 避免執行後續程式碼
        }

        // 確保索引在合法範圍內
        i = Mathf.Clamp(i, 0, Dialogues.Length - 1);
        DialogueText.text = Dialogues[i]; // 更新顯示的對話

        // 更新角色立繪圖片
        UpdateCharacterImage(i);
        UpdateTAGImage(i);
        UpdateCharacterName(i); // 更新角色名稱
    }

    // 更新角色立繪圖案的方法
    void UpdateCharacterImage(int index)
    {
        if (CharacterSprites.Length > index && CharacterImage != null)
        {
            CharacterImage.sprite = CharacterSprites[index]; // 根據對話索引切換角色立繪
        }
    }

    // 更新標籤圖片的方法
    void UpdateTAGImage(int index)
    {
        if (TAGSprites.Length > index && TAGImage != null)
        {
            TAGImage.sprite = TAGSprites[index]; // 根據對話索引切換標籤圖片
        }
    }

    // 新增：更新角色名稱的方法
    void UpdateCharacterName(int index)
    {
        if (CharacterNames.Length > index && CharacterNameText != null)
        {
            CharacterNameText.text = CharacterNames[index]; // 根據對話索引切換角色名稱
        }
    }
}
