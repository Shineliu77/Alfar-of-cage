using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skip : MonoBehaviour
{
    public string NextSceneName;          // 下一個場景的名稱

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Application.LoadLevel(NextSceneName);
        }
    }
}
