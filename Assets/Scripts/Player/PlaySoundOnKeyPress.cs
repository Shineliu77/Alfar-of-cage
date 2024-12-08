using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnKeyPress : MonoBehaviour
{

    public AudioClip skillSound;    // 指定技能音效
    public AudioSource audioSource; // 音效播放來源

    void Update()
    {
        // 當玩家按下 Q 鍵時
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 播放技能音效
            audioSource.PlayOneShot(skillSound);
        }
    }
}


