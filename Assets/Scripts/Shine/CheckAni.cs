using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAni : MonoBehaviour
{
    public Animator animator;
    public string animationStateName; // 動畫狀態名稱（如 "Idle"）
    public string NextGameName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 檢查是否為目標動畫狀態且進度接近完成
        if (stateInfo.IsName(animationStateName) && stateInfo.normalizedTime >= 1.0f)
        {
            Application.LoadLevel(NextGameName);
        }
    }
}
