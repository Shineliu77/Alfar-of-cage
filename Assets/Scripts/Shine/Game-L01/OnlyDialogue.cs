using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlyDialogue : MonoBehaviour
{
    public string[] Dialogues;          // ��ܤ��e
    private int i = 0;                  // ��e��ܯ���
    public Text DialogueText;           // �����ܤ�r����
    public GameObject[] NextObjs;       // �ݭn�ҥΪ��h�Ӫ���}�C
    public GameObject Player;           // Player ���� (�Ω��� PlayerControl �}��)
    private PlayerControl playerControl; // �ޥ� PlayerControl �}��

    void Start()
    {
        // �T�O Player ���� null �è��o PlayerControl �}��
        if (Player != null)
        {
            playerControl = Player.GetComponent<PlayerControl>();
            if (playerControl != null)
            {
                playerControl.enabled = false; // �C���}�l�ɸT�� PlayerControl
            }
        }

        if (Dialogues.Length > 0)
        {
            DialogueText.text = Dialogues[0]; // ��l�ƲĤ@�y���
        }
    }

    public void ClickNext()
    {
        i++;
        if (i >= Dialogues.Length)
        {
            gameObject.SetActive(false);

            // �@���ҥΰ}�C�����Ҧ�����
            foreach (GameObject obj in NextObjs)
            {
                if (obj != null)
                {
                    obj.SetActive(true); // �ҥΨC�ӳ]�m������
                }
            }

            // ��ܵ����ɱҥ� PlayerControl �}��
            if (playerControl != null)
            {
                playerControl.enabled = true;
            }

            return; // �קK�������{���X
        }

        i = Mathf.Clamp(i, 0, Dialogues.Length - 1); // �T�O���ަb�X�k�d��
        DialogueText.text = Dialogues[i];           // ��s��ܤ�r
    }
}
