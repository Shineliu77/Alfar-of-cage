using UnityEngine;

public class TriggerObjectScript : MonoBehaviour
{
    public Vector3 targetPosition;   // 設置指定的目標位置
    public float triggerRadius = 2f; // 判斷範圍，可以調整來設置觸發範圍
    public GameObject objectToEnable; // 要啟用的物件

    private bool hasTriggered = false; // 用來確保只觸發一次

    void Update()
    {
        // 計算玩家與目標位置的距離
        float distance = Vector3.Distance(transform.position, targetPosition);

        // 檢查玩家是否到達指定範圍
        if (distance <= triggerRadius && !hasTriggered)
        {
            Debug.Log("Player reached target position.");

            // 開啟指定的物件
            if (objectToEnable != null)
            {
                objectToEnable.SetActive(true);
            }

            hasTriggered = true; // 確保只觸發一次
        }
    }

}
