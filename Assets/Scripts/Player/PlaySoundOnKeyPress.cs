using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnKeyPress : MonoBehaviour
{

    public AudioClip skillSound;    // ���w�ޯ୵��
    public AudioSource audioSource; // ���ļ���ӷ�

    void Update()
    {
        // ���a���U Q ���
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // ����ޯ୵��
            audioSource.PlayOneShot(skillSound);
        }
    }
}


