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
        Destroy(gameObject, lifetime); // 一段時間後銷毀地刺
    }

    void Update()
    {
        // 左右來回移動
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);

        // 判斷是否超出移動範圍
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
                playerHealth.TakeDamage(10); // 假設地刺傷害是 10
            }
        }
    }
}
