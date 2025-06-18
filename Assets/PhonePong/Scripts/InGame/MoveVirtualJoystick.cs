// System
using System;


// Unity
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveVirtualJoystick : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private RectTransform lever;

    [SerializeField] private Transform top;
    [SerializeField] private Transform bottom;

    [SerializeField] private Racket racket;
    [SerializeField] private float racketMovableRange;

    [SerializeField] private float leverRange;
    [SerializeField] private float signLeverPosY;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        leverRange = rectTransform.sizeDelta.y * 0.5f;

        racketMovableRange = (top.position.y - bottom.position.y) * 0.5f;
    }

    public void OnDrag(PointerEventData eventData)
    {

        float newLeverPosY = eventData.position.y - rectTransform.position.y;
        float signNewLeverPosY = Math.Sign(newLeverPosY); // 현재 조이스틱을 당기고 있는 방향을 구한다.(1이면 위, -1이면 아래)

        newLeverPosY = Math.Abs(newLeverPosY) < leverRange ? newLeverPosY : signNewLeverPosY * leverRange;

        lever.anchoredPosition = new Vector2(lever.anchoredPosition.x, newLeverPosY);
        
        

        // if (signNewLeverPosY != signLeverPosY) // 조이스틱을 당기는 방향이 바뀔 때 호출 
        // {
        //     racket.Move(signLeverPosY = signNewLeverPosY);
        // }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // lever.anchoredPosition = Vector2.zero;
        // racket.Move(signLeverPosY = 0f);
    }
}