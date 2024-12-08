using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneTreeToggle : MonoBehaviour
{
    [Header("�ޯ�d��]�w")]
    public float skillRadius = 5f; // �����d��b�|

    [Header("���ҳ]�w")]
    public string deadZoneTag = "DeadZone"; // DeadZone ����
    public string treeTag = "tree"; // �𪺼���

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // ���U Q ��Ĳ�o�ޯ�
        {
            ToggleDeadZoneAndTree();
        }
    }

    void ToggleDeadZoneAndTree()
    {
        // �˴��d�򤺩Ҧ����I����
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, skillRadius);

        foreach (Collider2D hitCollider in hitColliders)
        {
            // �ˬd�O�_�� DeadZone ���Ҫ�����
            if (hitCollider.CompareTag(deadZoneTag))
            {
                // ���� DeadZone ����
                Debug.Log($"Disabling DeadZone: {hitCollider.name}");
                hitCollider.gameObject.SetActive(false);

                // �ҥΦP�@������U�� tree ���Ҫ���
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
        // ø�s�ޯ�d�򪺥i���ƻ��U�u
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, skillRadius);
    }
}
