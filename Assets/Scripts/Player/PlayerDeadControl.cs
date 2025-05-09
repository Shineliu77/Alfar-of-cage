using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            Die();
        }
    }
    public void Die()
    {
        // ���a���`�B�z�޿�
        Debug.Log("Player Died");

        // ����C���G�N�ɶ��]�m�� 0�]�Ȱ��C���^
        Time.timeScale = 0;

        // ����a���ާ@�G�T�Ϊ��z�M�I���A�O�����a����s�b�����i��
        SpriteRenderer[] renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
        Collider2D[] colliders = gameObject.GetComponentsInChildren<Collider2D>();
        Rigidbody2D[] rigidbodies = gameObject.GetComponentsInChildren<Rigidbody2D>();

        // �T�δ�V�B�I���M���z
        foreach (var renderer in renderers)
        {
            renderer.enabled = false;
        }
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = true;
        }

        // ��� Game Over �e��
        GameObject.FindObjectOfType<GameOverManager>().ShowGameOver();
    }
}
