using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSoundController : MonoBehaviour
{
    public Animator animator;          // 動畫控制器
    public string targetAnimation;    // 指定動畫名稱
    public AudioClip soundEffect;     // 指定音效
    public AudioSource audioSource;   // 音效播放來源

    private bool hasPlayedSound = false; // 用於避免重複播放音效

    void Update()
    {
        // 檢查動畫是否處於目標動畫
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(targetAnimation))
        {
            // 如果動畫正在播放且音效尚未播放
            if (!hasPlayedSound && !audioSource.isPlaying)
            {
                audioSource.PlayOneShot(soundEffect); // 播放音效
                hasPlayedSound = true;               // 記錄音效已播放
            }
        }
        else
        {
            // 如果動畫不再播放，重置音效播放狀態
            hasPlayedSound = false;
        }
    }
}
