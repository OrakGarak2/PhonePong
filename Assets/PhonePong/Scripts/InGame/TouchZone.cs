// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

public class TouchZone : MonoBehaviour
{
    protected const int maxTouchCount = 4;

    [SerializeField] protected int layerBitMask;

    protected virtual void Start()
    {
        layerBitMask = 1 << gameObject.layer;
    }

    protected virtual bool CheckPointInZone(out Vector2 pointPos)
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (i >= maxTouchCount) break;

            pointPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
            Collider2D hit = Physics2D.OverlapPoint(pointPos, layerBitMask);

            Debug.Log($"Object: {gameObject.name}\ni: {i}, pointPos: {pointPos}, hit: {hit != null}, hit && hit.gameObject == gameObject: {hit && hit.gameObject == gameObject}");

            if (hit && hit.gameObject == gameObject)
            {
                return true;
            }
        }

        pointPos = Vector2.zero;
        return false;
    }
}
