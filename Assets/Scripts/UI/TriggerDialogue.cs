using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerDialogue : MonoBehaviour
{
    public GameObject DialogueObject;    // Ĳ�o��ܪ�����

    void Start()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //�}�ҹ�ܪ���
            DialogueObject.SetActive(true);
            //�����IĲ������
            this.gameObject.SetActive(false);
        }
    }
   
}
