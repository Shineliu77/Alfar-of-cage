using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterSwitch : MonoBehaviour
{
    public GameObject[] characters; // �Ҧ����⪺ GameObject
    public Button[] characterCards; // ����d�P�� UI ���s
    public int defaultCharacterIndex = 0; // �w�]��ܪ��������

    private void Start()
    {
        // ��ܹw�]����
        ShowCharacter(defaultCharacterIndex);

        // �]�m���s�ƥ�
        for (int i = 0; i < characterCards.Length; i++)
        {
            int index = i; // �ϥΧ����ܼƨ���]���D
            characterCards[i].onClick.AddListener(() => ShowCharacter(index));
        }
    }

    // ��ܿ襤������A�����è�L����
    void ShowCharacter(int index)
    {
        // ���éҦ�����
        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }

        // ��ܿ襤������
        characters[index].SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);

    }
}
