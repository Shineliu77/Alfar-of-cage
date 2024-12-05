using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerDialogueAndAnimation : MonoBehaviour
{
    public GameObject DialogueObject;    // 觸發對話的物件
    public Animator AnimationPlayer;     // 播放動畫的 Animator
    public string AnimationName;         // 指定的動畫名稱
    public string NextSceneName;         // 下一個場景的名稱
    public float AnimationEndThreshold = 0.95f; // 動畫結束的時間閾值

    private bool isDialogueActive = false;
    private bool isAnimationPlaying = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isDialogueActive)
        {
            // 開啟對話物件
            DialogueObject.SetActive(true);
            isDialogueActive = true;

            // 關閉觸發器物件
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // 檢查對話是否已結束
        if (isDialogueActive && !DialogueObject.activeInHierarchy)
        {
            isDialogueActive = false;
            PlayAnimation();
        }

        // 檢查動畫是否播放完成
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
