using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class OnlyDialogue : MonoBehaviour
{
    public string[] Dialogues;          // ��ܤ��e
    private int i = 0;                  // ��e��ܯ���
    public Text DialogueText;           // �Ω���ܹ�ܤ�r�� UI Text ����

    public GameObject[] NextObjs;       // �ݭn�ҥΪ��h�Ӫ���}�C
    public GameObject Player;           // Player ���� (�ΨӨ��o PlayerControl �}��)
    private PlayerControl playerControl; // �ޥ� PlayerControl �}��
    public Image CharacterImage;        // �Ω���ܨ����ø�� UI Image ����
    public Image TAGImage;
    public Sprite[] CharacterSprites;   // �s�x�C�ӹ�ܹ����������ø�Ϥ�
    public Sprite[] TAGSprites;

    void Start()
    {
        // �T�O Player ���󤣬� null�A�è��o PlayerControl �}��
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
            DialogueText.text = Dialogues[0]; // ��ܲĤ@�y���
            UpdateCharacterImage(0); // ��ܹ����������ø
            UpdateTAGImage(0);
        }
    }

    public void ClickNext()
    {
        i++; // �e�i��U�@�y���
        if (i >= Dialogues.Length)
        {
            gameObject.SetActive(false); // ���ù�ܮ�

            // �ҥΰ}�C�����Ҧ�����
            foreach (GameObject obj in NextObjs)
            {
                if (obj != null)
                {
                    obj.SetActive(true); // �ҥΨC�Ӫ���
                }
            }

            // ��ܵ�����A���s�ҥ� PlayerControl
            if (playerControl != null)
            {
                playerControl.enabled = true;
            }

            return; // �קK�������{���X
        }

        // �T�O���ަb�X�k�d��
        i = Mathf.Clamp(i, 0, Dialogues.Length - 1);
        DialogueText.text = Dialogues[i]; // ��s��ܪ����

        // ��s�����ø�Ϥ�
        UpdateCharacterImage(i);
        UpdateTAGImage(i);

    }

    // ��s�����ø�Ϯת���k
    void UpdateCharacterImage(int index)
    {
        if (CharacterSprites.Length > index && CharacterImage != null)
        {
            CharacterImage.sprite = CharacterSprites[index]; // �ھڹ�ܯ��ޤ��������ø
        }
    }

    void UpdateTAGImage(int index)
    {
        if (TAGSprites.Length > index && TAGImage != null)
        {
            TAGImage.sprite = TAGSprites[index]; // �ھڹ�ܯ��ޤ��������ø
        }
    }
}
