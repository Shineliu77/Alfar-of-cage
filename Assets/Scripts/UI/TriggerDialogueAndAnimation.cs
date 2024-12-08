using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerDialogueAndAnimation : MonoBehaviour
{
    public GameObject DialogueObject;    // ��ܪ���
    public GameObject BlackScreenObject; // �¹�����

    private bool isDialogueActive = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isDialogueActive)
        {
            // �}�ҹ�ܪ���
            DialogueObject.SetActive(true);
            isDialogueActive = true;

            // ����Ĳ�o������
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // �ˬd��ܪ���O�_�Q����
        if (isDialogueActive && !DialogueObject.activeSelf)
        {
            Debug.Log("fkudio");
            // ��ܵ����A�ҥζ¹�����
            if (BlackScreenObject != null)
            {
                Debug.Log("fkublack");
                BlackScreenObject.SetActive(true);
            }
            isDialogueActive = false;
        }
    }
}
