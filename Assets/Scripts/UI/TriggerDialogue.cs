using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerDialogue : MonoBehaviour
{
    public string[] Dialogues;          // ��ܤ��e
    private int i = 0;                  // ��e��ܯ���
    public Text DialogueText;           // �����ܤ�r����
    public GameObject[] ObjectsToDisable; // �ݭn�T�Ϊ�����}�C
    public GameObject Player;           // Player ���� (�Ω��� PlayerControl �}��)
    private PlayerControl playerControl; // �ޥ� PlayerControl �}��
    public GameObject TriggerObject;    // Ĳ�o��ܪ�����

    private bool isDialogueActive = false; // �P�_��ܬO�_���b�i��

    void Start()
    {
        Debug.Log("fku");
        // �T�O Player ���� null �è��o PlayerControl �}��
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

        // �T�� PlayerControl
        if (playerControl != null)
        {
            playerControl.enabled = false;
        }

        // �T�ΰ}�C�����Ҧ�����
        foreach (GameObject obj in ObjectsToDisable)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }

        // ��l�ƲĤ@�y���
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
            return; // �קK�������{���X
        }

        i = Mathf.Clamp(i, 0, Dialogues.Length - 1); // �T�O���ަb�X�k�d��
        DialogueText.text = Dialogues[i];           // ��s��ܤ�r
    }

    void EndDialogue()
    {
        isDialogueActive = false;

        // �ҥ� PlayerControl
        if (playerControl != null)
        {
            playerControl.enabled = true;
        }

        // �ҥΰ}�C�����Ҧ�����
        foreach (GameObject obj in ObjectsToDisable)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }

        // �M�Ź�ܤ�r
        DialogueText.text = "";

        // ���m��ܯ���
        i = 0;
    }
}
