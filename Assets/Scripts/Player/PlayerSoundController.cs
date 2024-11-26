using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    [Header("音效設置")]
    public AudioClip walkSound; // 走路音效
    public AudioClip jumpSound; // 跳躍音效
    public AudioClip skillSound; // 技能音效
    public float walkSoundInterval = 0.3f; // 走路音效間隔

    private AudioSource audioSource;
    private float lastWalkSoundTime; // 上次播放走路音效的時間
    private bool isWalking; // 是否正在走路

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        HandleMovementSounds();
        HandleActionSounds();
    }

    private void HandleMovementSounds()
    {
        // 判斷玩家是否正在移動（按下 D 或 A）
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            isWalking = true;

            // 控制走路音效的播放間隔
            if (Time.time - lastWalkSoundTime >= walkSoundInterval)
            {
                PlaySoundInterrupt(walkSound); // 播放並中斷前一次音效
                lastWalkSoundTime = Time.time;
            }
        }
        else if (isWalking)
        {
            // 玩家停止移動時
            isWalking = false;
            StopSound(); // 停止播放音效
        }
    }

    private void HandleActionSounds()
    {
        // 按空白鍵播放跳躍音效
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaySoundInterrupt(jumpSound);
        }

        // 按 Q 播放技能音效
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlaySoundInterrupt(skillSound);
        }
    }

    private void PlaySoundInterrupt(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.Stop(); // 停止當前音效
            audioSource.PlayOneShot(clip); // 播放新的音效
        }
    }

    private void StopSound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop(); // 停止當前播放中的音效
        }
    }
}
