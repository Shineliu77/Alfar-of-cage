using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected bool isPaused = false;
    protected float pauseTimer = 0f;

    public float normalspeed;//�@��t��
    public float chasespeed;//�l���t��
    public float currentspeed;//��e�t��
    public Collider2D enemy;//�ĤH���I����
    public Transform pointA, pointB; //�����IAB
    protected Vector3 targetPosition; // ��e�ؼЦ�m
    protected int faceDir;//�����V
    

    protected void Awake()
    {
        currentspeed = normalspeed;
        targetPosition = pointA.position; // ��l�ƥؼЦ�m���I A
    }

    protected void Update()
    {
        if (isPaused)
        {
            pauseTimer -= Time.deltaTime;
            enemy.enabled = false;
            if (pauseTimer <= 0)
            {
                isPaused = false;
                enemy.enabled = true;
            }
            return;
        }

        // ��{�ĤH�����ʩΧ����欰
        Move();
    }


    protected void Move()
    {
        // ���ʼĤH��ؼЦ�m
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentspeed * Time.deltaTime);

        // �ˬd�O�_��F�ؼЦ�m
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // ����ؼЦ�m
            targetPosition = targetPosition == pointA.position ? pointB.position : pointA.position;
        }

        if (targetPosition == pointA.position)
            faceDir = 1;
        if (targetPosition == pointB.position)
            faceDir = -1;

        //�H��½��
        transform.localScale = new Vector3(faceDir, 1, 1);
    }

    protected void OnTriggerEnter2D(Collider2D haveplayer)
    {

        if (currentspeed == normalspeed && haveplayer.gameObject.CompareTag("Gameplayer"))
        {
            currentspeed = chasespeed;
        }
        return;

    }

    protected void OnTriggerExit2D(Collider2D haveplayer)
    {

        if (currentspeed == chasespeed && haveplayer.gameObject.CompareTag("Gameplayer"))
        {
            currentspeed = normalspeed;
        }
        return;

    }

    public void Pause(float duration)
    {
        isPaused = true;
        pauseTimer = duration;
    }
}
