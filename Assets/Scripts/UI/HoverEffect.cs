using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    private Vector3 originalPosition;
    public float hoverScale = 1.1f; // �B�_�Ӫ���j����
    public float hoverYOffset = 10f; // �B�_�ӮɦV�W���ʪ��Z��
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
        originalPosition = rectTransform.localPosition;
    }

    // ��ƹ��i�J�ɩ�j�æV�W����
    public void OnPointerEnter(PointerEventData eventData)
    {
        rectTransform.localScale = originalScale * hoverScale;
        rectTransform.localPosition = originalPosition + new Vector3(0, hoverYOffset, 0); // �V�W����
    }

    // ��ƹ����}�ɫ�_��Ӥj�p�M��m
    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.localScale = originalScale;
        rectTransform.localPosition = originalPosition; // ��_��Ӫ���m
    }
}

