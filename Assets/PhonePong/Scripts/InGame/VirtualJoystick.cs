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

    [SerializeField] private Bar bar;

    [SerializeField] private float leverRange;
    [SerializeField] private float signLeverPosY;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        leverRange = rectTransform.sizeDelta.y * 0.5f;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // float leverPosY = eventData.position.y - rectTransform.position.y;

        // leverPosY = Math.Abs(leverPosY) < leverRange ? leverPosY : Math.Sign(leverPosY) * leverRange;

        // lever.anchoredPosition = new Vector2(lever.anchoredPosition.x, leverPosY);
    }

    public void OnDrag(PointerEventData eventData)
    {
        float newLeverPosY = eventData.position.y - rectTransform.position.y;
        float signNewLeverPosY = Math.Sign(newLeverPosY);

        newLeverPosY = Math.Abs(newLeverPosY) < leverRange ? newLeverPosY : signNewLeverPosY * leverRange;

        lever.anchoredPosition = new Vector2(lever.anchoredPosition.x, newLeverPosY);

        if (signNewLeverPosY != signLeverPosY)
        {
            bar.Move(signLeverPosY = signNewLeverPosY);
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
        bar.Move(signLeverPosY = 0f);
    }
}