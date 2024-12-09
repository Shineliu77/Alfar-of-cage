using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAni : MonoBehaviour
{
    public Animator animator;
    public string animationStateName; // �ʵe���A�W�١]�p "Idle"�^
    public string NextGameName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // �ˬd�O�_���ؼаʵe���A�B�i�ױ��񧹦�
        if (stateInfo.IsName(animationStateName) && stateInfo.normalizedTime >= 1.0f)
        {
            Application.LoadLevel(NextGameName);
        }
    }
}
