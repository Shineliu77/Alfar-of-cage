using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool isPaused = false;
    private float pauseTimer = 0f;

    public float moveSpeed = 2f; // �ĤH���ʳt��
    public Transform pointA; // �����I A
    public Transform pointB; // �����I B
    private Vector3 targetPosition; // ��e�ؼЦ�m

    void Start()
    {
        targetPosition = pointA.position; // ��l�ƥؼЦ�m���I A
    }

    void Update()
    {
        if (isPaused)
        {
            pauseTimer -= Time.deltaTime;
            if (pauseTimer <= 0)
            {
                isPaused = false;
            }
            return;
        }

        // ��{�ĤH�����ʩΧ����欰
        Move();
    }

    private void Move()
    {
        // ���ʼĤH��ؼЦ�m
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // �ˬd�O�_��F�ؼЦ�m
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // ����ؼЦ�m
            targetPosition = targetPosition == pointA.position ? pointB.position : pointA.position;
        }

        int faceDir = (int)transform.localScale.x;

        transform.localScale = new Vector3(faceDir, 1, 1);
    }

    

    public void Pause(float duration)
    {
        isPaused = true;
        pauseTimer = duration;
    }
}
