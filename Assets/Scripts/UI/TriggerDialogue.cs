using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    public GameObject DialogueObject;    // Ĳ�o��ܪ�����
    public static Vector3 lastDialoguePosition; // �O�s�̫�Ĳ�o��ܪ���m
    private bool hasTriggered = false;  // �T�O�uĲ�o�@��

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            // �}�ҹ�ܪ���
            DialogueObject.SetActive(true);
            // �O�s��e��m�����ͦ�m
            lastDialoguePosition = transform.position;
            // �����IĲ������
            this.gameObject.SetActive(false);
        }
    }
}
