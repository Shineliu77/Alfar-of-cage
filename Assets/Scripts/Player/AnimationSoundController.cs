using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSoundController : MonoBehaviour
{
    public Animator animator;          // �ʵe���
    public string targetAnimation;    // ���w�ʵe�W��
    public AudioClip soundEffect;     // ���w����
    public AudioSource audioSource;   // ���ļ���ӷ�

    private bool hasPlayedSound = false; // �Ω��קK���Ƽ��񭵮�

    void Update()
    {
        // �ˬd�ʵe�O�_�B��ؼаʵe
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(targetAnimation))
        {
            // �p�G�ʵe���b����B���ĩ|������
            if (!hasPlayedSound && !audioSource.isPlaying)
            {
                audioSource.PlayOneShot(soundEffect); // ���񭵮�
                hasPlayedSound = true;               // �O�����Ĥw����
            }
        }
        else
        {
            // �p�G�ʵe���A����A���m���ļ��񪬺A
            hasPlayedSound = false;
        }
    }
}
