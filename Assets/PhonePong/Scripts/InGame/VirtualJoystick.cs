// System
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;


// Unity
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private RectTransform lever;

    [SerializeField] private Racket racket;

    [SerializeField] private float leverRange;
    [SerializeField] private float signLeverPosY;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        leverRange = rectTransform.sizeDelta.y * 0.5f;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // TODO: 나중에도 안쓰면 삭제할 것
    }

    public void OnDrag(PointerEventData eventData)
    {
        float newLeverPosY = eventData.position.y - rectTransform.position.y;
        float signNewLeverPosY = Math.Sign(newLeverPosY);

        newLeverPosY = Math.Abs(newLeverPosY) < leverRange ? newLeverPosY : signNewLeverPosY * leverRange;

        lever.anchoredPosition = new Vector2(lever.anchoredPosition.x, newLeverPosY);

        if (signNewLeverPosY != signLeverPosY)
        {
            racket.Move(signLeverPosY = signNewLeverPosY);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
        racket.Move(signLeverPosY = 0f);
    }
}