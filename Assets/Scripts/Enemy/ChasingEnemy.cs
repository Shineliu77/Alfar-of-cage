using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : MonoBehaviour
{

    public float detectionRange = 10f;        // �������a���d��
    public float normalSpeed = 3f;           // ���`���ʳt��
    public float dashSpeed = 10f;            // �Ĩ�t��
    public float attackCooldown = 2f;        // �C�������᪺�N�o�ɶ�
    public float retreatDistance = 3f;       // ������h�᪺�Z��
    public float baseDamage = 5f;            // ��¦�ˮ`
    public float maxDamage = 20f;            // �̤j�ˮ`
    public float movementRange = 10f;        // �ĤH�i���ʪ��̤j�d��

    private Transform player;
    private Rigidbody2D rb;
    private bool isDashing = false;
    private bool canAttack = true;
    private Vector2 initialPosition;         // ��l��m�A�Ω�d�򭭨�
    private Vector2 lastPlayerPosition;      // �̫�@���Ĩ�ɪ��a����m

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position; // �O���ĤH����l��m
    }

    void Update()
    {
        if (player == null)
            return;

        // �ˬd���a�O�_�b�����d��
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange && canAttack)
        {
            // ���a�i�J�d��B�i�H�����A�i��Ĩ�
            StartDash();
        }
        else if (!isDashing)
        {
            // �p�G���a���b�d�򤺡A�^���l��m
            PatrolOrReturn();
        }
    }

    void StartDash()
    {
        isDashing = true;
        canAttack = false;
        lastPlayerPosition = player.position;  // �ഫ player.position �� Vector2

        // �p��Ĩ��V�ó]�m�t��
        Vector2 dashDirection = (lastPlayerPosition - (Vector2)transform.position).normalized;

        // �T�O�ĤH���|�ĥX���ʽd��
        Vector2 predictedPosition = (Vector2)transform.position + dashDirection * dashSpeed * Time.deltaTime;
        if (Vector2.Distance(initialPosition, predictedPosition) <= movementRange)
        {
            rb.velocity = dashDirection * dashSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero; // �p�G�|�W�X�d��A�����Ĩ�
            isDashing = false;
        }
    }

    void PatrolOrReturn()
    {
        // �p�G�W�X���ʽd��A��^��l��m
        if (Vector2.Distance(initialPosition, transform.position) > movementRange)
        {
            Vector2 returnDirection = (initialPosition - (Vector2)transform.position).normalized;
            rb.velocity = returnDirection * normalSpeed;
        }
        else
        {
            // �b�d�򤺨��ޡ]�i�H�[�W�����޿�^
            rb.velocity = Vector2.zero; // �o�̥i�H�K�[���ަ欰
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDashing && collision.gameObject.CompareTag("Player"))
        {
            // ����Ĩ�
            isDashing = false;
            rb.velocity = Vector2.zero;

            // �p��Ĩ�ˮ`�A�ھڽĨ�t�׼W�[�ˮ`
            float impactSpeed = dashSpeed;  // ���]�Ĩ�ɪ��̤j�t��
            float damage = baseDamage + (impactSpeed / dashSpeed) * maxDamage;

            // �缾�a�y���ˮ`
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // �}�l�h��öi������N�o
            StartCoroutine(RetreatAndCooldown());
        }
    }

    System.Collections.IEnumerator RetreatAndCooldown()
    {
        // �h��
        Vector2 retreatDirection = ((Vector2)transform.position - lastPlayerPosition).normalized;
        rb.velocity = retreatDirection * normalSpeed;

        // ���ݤ@�q�ɶ��H�����h��
        yield return new WaitForSeconds(0.5f);

        // ����h��
        rb.velocity = Vector2.zero;

        // ���ݧ����N�o
        yield return new WaitForSeconds(attackCooldown);

        // ��_������O
        canAttack = true;
    }
}
