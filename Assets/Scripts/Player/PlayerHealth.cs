using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth = 100;
    private Rigidbody2D rb;
    [SerializeField]private PlayerControl control;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // ���a����ˮ`
    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            Debug.Log("Player Died");
            // ���a���`�޿�
        }

        // �缾�a�I�[���h�ĪG
        ApplyKnockBack();
    }

 
    
        [Header("���h�Z��"), SerializeField] private float distance;
        [Header("���h�ɶ�"), SerializeField] private float time;
      
        private Vector3 startPosition;
        private Vector3 endPosition;
        private float timer;
        private bool isKnockBack;

        public void ApplyKnockBack()
        {
            isKnockBack = true;

            Vector3 direction = control.FaceDir == 1 ? Vector3.left : Vector3.right;

            // if (faceDir == 1)
            // {
            //     direction = Vector3.left;
            // }
            // else
            // {
            //     direction = Vector3.right;
            // }

            startPosition = transform.position;
            endPosition = startPosition + direction * distance;
            transform.position = endPosition;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ApplyKnockBack();
            }

            if (isKnockBack)
            {
                timer += Time.deltaTime;
                transform.position = Vector3.Lerp(startPosition, endPosition, timer / time);

                if (timer >= time)
                {
                    isKnockBack = false;
                    timer = 0;
                }
            }
        }
    


}
