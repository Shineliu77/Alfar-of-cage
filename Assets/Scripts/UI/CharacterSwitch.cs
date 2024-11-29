using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterSwitch : MonoBehaviour
{
    public GameObject[] characters; // 所有角色的 GameObject
    public Button[] characterCards; // 角色卡牌的 UI 按鈕
    public int defaultCharacterIndex = 0; // 預設顯示的角色索引

    private void Start()
    {
        // 顯示預設角色
        ShowCharacter(defaultCharacterIndex);

        // 設置按鈕事件
        for (int i = 0; i < characterCards.Length; i++)
        {
            int index = i; // 使用局部變數防止閉包問題
            characterCards[i].onClick.AddListener(() => ShowCharacter(index));
        }
    }

    // 顯示選中的角色，並隱藏其他角色
    void ShowCharacter(int index)
    {
        // 隱藏所有角色
        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }

        // 顯示選中的角色
        characters[index].SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);

    }
}
