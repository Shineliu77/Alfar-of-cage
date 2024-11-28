using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundWater : MonoBehaviour
{
    public bool playerInRange; // ���a�O�_�b�d��
    public bool isActive = true; // GroundWater �O�_�ҥ�

    [Header("���`�]�w")]
    public float deathDelay = 0f; // ��Ĳ�᩵��X���`�]�p�G�ݭn�^

    void Update()
    {
        // �p�G���a�b�d�򤺨åB GroundWater �O�ҥΪ��A
        if (playerInRange && isActive)
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        // �d�䪱�a�}���ýեΦ��`��k
        PlayerControl player = GameObject.FindObjectOfType<PlayerControl>();
        if (player != null)
        {
            isActive = false; // �T�� GroundWater�]����h��Ĳ�o�^
            player.Die(); // Ĳ�o���a���`�޿�
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �˴����a�O�_�i�J�d��
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // �˴����a�O�_���}�d��
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
