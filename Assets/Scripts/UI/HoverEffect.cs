using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    private Vector3 originalPosition;
    public float hoverScale = 1.1f; // 浮起來的放大倍數
    public float hoverYOffset = 10f; // 浮起來時向上移動的距離
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
        originalPosition = rectTransform.localPosition;
    }

    // 當滑鼠進入時放大並向上移動
    public void OnPointerEnter(PointerEventData eventData)
    {
        rectTransform.localScale = originalScale * hoverScale;
        rectTransform.localPosition = originalPosition + new Vector3(0, hoverYOffset, 0); // 向上移動
    }

    // 當滑鼠離開時恢復原來大小和位置
    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.localScale = originalScale;
        rectTransform.localPosition = originalPosition; // 恢復原來的位置
    }
}

