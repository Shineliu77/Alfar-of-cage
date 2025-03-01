using System.Collections;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float moveDistance = 2f;
    public float lifetime = 5f;

    private Vector2 startPos;
    private int direction = 1;

    void Start()
    {
        startPos = transform.position;
        Destroy(gameObject, lifetime); // �@�q�ɶ���P���a��
    }

    void Update()
    {
        // ���k�Ӧ^����
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);

        // �P�_�O�_�W�X���ʽd��
        if (Mathf.Abs(transform.position.x - startPos.x) >= moveDistance)
        {
            direction *= -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10); // ���]�a��ˮ`�O 10
            }
        }
    }
}
