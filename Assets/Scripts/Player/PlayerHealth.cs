using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("���a��q�]�w")]
    public int playerHealth = 100; // ��e��q
    public int maxHealth = 100; // �̤j��q

    [Header("��� UI")]
    public Slider healthSlider; // ��� Slider

    [Header("���h�]�w")]
    public float knockbackDistance = 2f; // ���h�Z��
    public float knockbackTime = 0.2f; // ���h�ɶ�
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float timer;
    private bool isKnockback;

    [Header("���˭���")]
    public AudioSource damageAudio;

    private Rigidbody2D rb;
    [SerializeField] private PlayerControl control;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // ��l�Ʀ��
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = playerHealth;
        }
    }

    void Update()
    {
        // ���h�޿�
        if (isKnockback)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, timer / knockbackTime);

            if (timer >= knockbackTime)
            {
                isKnockback = false;
                timer = 0;
            }
        }
    }

    // ���a����ˮ`
    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        playerHealth = Mathf.Clamp(playerHealth, 0, maxHealth);

        // ������˭���
        if (damageAudio != null)
        {
            damageAudio.Play();
        }

        // ��s���
        if (healthSlider != null)
        {
            healthSlider.value = playerHealth;
        }

        // �P�_���a�O�_���`
        if (playerHealth <= 0)
        {
            Debug.Log("Player Died");
            GameObject.FindObjectOfType<GameOverManager>()?.ShowGameOver();
        }

        // �I�[���h�ĪG
        ApplyKnockback();
    }

    // ���h�޿�
    public void ApplyKnockback()
    {
        isKnockback = true;

        Vector3 direction = control.FaceDir == 1 ? Vector3.left : Vector3.right;
        startPosition = transform.position;
        endPosition = startPosition + direction * knockbackDistance;
    }
}
