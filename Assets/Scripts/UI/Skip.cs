using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skip : MonoBehaviour
{
    public string NextSceneName;          // �U�@�ӳ������W��

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Application.LoadLevel(NextSceneName);
        }
    }
}
