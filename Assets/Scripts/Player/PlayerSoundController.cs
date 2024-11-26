using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    [Header("���ĳ]�m")]
    public AudioClip walkSound; // ��������
    public AudioClip jumpSound; // ���D����
    public AudioClip skillSound; // �ޯ୵��
    public float walkSoundInterval = 0.3f; // �������Ķ��j

    private AudioSource audioSource;
    private float lastWalkSoundTime; // �W�����񨫸����Ī��ɶ�
    private bool isWalking; // �O�_���b����

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
        // �P�_���a�O�_���b���ʡ]���U D �� A�^
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            isWalking = true;

            // ��������Ī����񶡹j
            if (Time.time - lastWalkSoundTime >= walkSoundInterval)
            {
                PlaySoundInterrupt(walkSound); // ����ä��_�e�@������
                lastWalkSoundTime = Time.time;
            }
        }
        else if (isWalking)
        {
            // ���a����ʮ�
            isWalking = false;
            StopSound(); // ����񭵮�
        }
    }

    private void HandleActionSounds()
    {
        // ���ť��伽����D����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaySoundInterrupt(jumpSound);
        }

        // �� Q ����ޯ୵��
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlaySoundInterrupt(skillSound);
        }
    }

    private void PlaySoundInterrupt(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.Stop(); // �����e����
            audioSource.PlayOneShot(clip); // ����s������
        }
    }

    private void StopSound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop(); // �����e���񤤪�����
        }
    }
}
