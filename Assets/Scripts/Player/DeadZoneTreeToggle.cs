using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneTreeToggle : MonoBehaviour
{
    [Header("技能範圍設定")]
    public float skillRadius = 5f; // 偵測範圍半徑

    [Header("標籤設定")]
    public string deadZoneTag = "DeadZone"; // DeadZone 標籤
    public string treeTag = "tree"; // 樹的標籤

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // 按下 Q 鍵觸發技能
        {
            ToggleDeadZoneAndTree();
        }
    }

    void ToggleDeadZoneAndTree()
    {
        // 檢測範圍內所有的碰撞體
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, skillRadius);

        foreach (Collider2D hitCollider in hitColliders)
        {
            // 檢查是否有 DeadZone 標籤的物件
            if (hitCollider.CompareTag(deadZoneTag))
            {
                // 關閉 DeadZone 物件
                Debug.Log($"Disabling DeadZone: {hitCollider.name}");
                hitCollider.gameObject.SetActive(false);

                // 啟用同一父物件下的 tree 標籤物件
                Transform parentTransform = hitCollider.transform.parent;
                if (parentTransform != null)
                {
                    foreach (Transform sibling in parentTransform)
                    {
                        if (sibling.CompareTag(treeTag))
                        {
                            Debug.Log($"Enabling tree: {sibling.name}");
                            sibling.gameObject.SetActive(true);
                        }
                    }
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // 繪製技能範圍的可視化輔助線
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, skillRadius);
    }
}
