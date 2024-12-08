using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerDialogueAndAnimation : MonoBehaviour
{
    public GameObject DialogueObject;    // 對話物件
    public GameObject BlackScreenObject; // 黑幕物件

    private bool isDialogueActive = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isDialogueActive)
        {
            // 開啟對話物件
            DialogueObject.SetActive(true);
            isDialogueActive = true;

            // 關閉觸發器物件
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // 檢查對話物件是否被關閉
        if (isDialogueActive && !DialogueObject.activeSelf)
        {
            Debug.Log("fkudio");
            // 對話結束，啟用黑幕物件
            if (BlackScreenObject != null)
            {
                Debug.Log("fkublack");
                BlackScreenObject.SetActive(true);
            }
            isDialogueActive = false;
        }
    }
}
