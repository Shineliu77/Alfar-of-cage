using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerDialogueAndAnimation : MonoBehaviour
{
    public GameObject DialogueObject;    // Ĳ�o��ܪ�����
    public Animator AnimationPlayer;     // ����ʵe�� Animator
    public string AnimationName;         // ���w���ʵe�W��
    public string NextSceneName;         // �U�@�ӳ������W��
    public float AnimationEndThreshold = 0.95f; // �ʵe�������ɶ��H��

    private bool isDialogueActive = false;
    private bool isAnimationPlaying = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isDialogueActive)
        {
            // �}�ҹ�ܪ���
            DialogueObject.SetActive(true);
            isDialogueActive = true;

            // ����Ĳ�o������
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // �ˬd��ܬO�_�w����
        if (isDialogueActive && !DialogueObject.activeInHierarchy)
        {
            isDialogueActive = false;
            PlayAnimation();
        }

        // �ˬd�ʵe�O�_���񧹦�
        if (isAnimationPlaying && AnimationPlayer.GetCurrentAnimatorStateInfo(0).IsName(AnimationName))
        {
            if (AnimationPlayer.GetCurrentAnimatorStateInfo(0).normalizedTime > AnimationEndThreshold)
            {
                isAnimationPlaying = false;
                LoadNextScene();
            }
        }
    }

    private void PlayAnimation()
    {
        if (AnimationPlayer != null && !string.IsNullOrEmpty(AnimationName))
        {
            isAnimationPlaying = true;
            AnimationPlayer.Play(AnimationName);
        }
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(NextSceneName))
        {
            SceneManager.LoadScene(NextSceneName);
        }
    }
}
