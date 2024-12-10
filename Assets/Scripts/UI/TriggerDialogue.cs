using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    public GameObject DialogueObject;    // 觸發對話的物件
    public static Vector3 lastDialoguePosition; // 保存最後觸發對話的位置
    private bool hasTriggered = false;  // 確保只觸發一次

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            // 開啟對話物件
            DialogueObject.SetActive(true);
            // 保存當前位置為重生位置
            lastDialoguePosition = transform.position;
            // 關閉碰觸的物件
            this.gameObject.SetActive(false);
        }
    }
}
