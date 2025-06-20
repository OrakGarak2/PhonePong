// System
using System;


// Unity
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveVirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private RectTransform lever;

    [SerializeField] private Transform top;
    [SerializeField] private Transform bottom;

    [SerializeField] private Racket racket;
    [SerializeField] private float racketMovableRange;

    [SerializeField] private float leverRange;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        leverRange = (rectTransform.sizeDelta.y - lever.sizeDelta.y) * 0.5f;

        racketMovableRange = (top.position.y - bottom.position.y) * 0.5f;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Move(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Move(eventData);
    }

    private void Move(PointerEventData eventData)
    {
        float newLeverPosY = eventData.position.y - rectTransform.position.y;
        float signNewLeverPosY = Math.Sign(newLeverPosY); // 현재 조이스틱을 당기고 있는 방향을 구한다.(1이면 위, -1이면 아래)

        newLeverPosY = Math.Abs(newLeverPosY) < leverRange ? newLeverPosY : signNewLeverPosY * leverRange;

        lever.anchoredPosition = new Vector2(lever.anchoredPosition.x, newLeverPosY);

        racket.Move(racketMovableRange * newLeverPosY / leverRange);
    }
}