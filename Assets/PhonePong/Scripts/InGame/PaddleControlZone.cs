// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

public class PaddleControlZone : TouchZone
{
    [SerializeField] private Paddle paddle;

    private void Update()
    {
        if (CheckPointInZone(out Vector2 pointPos))
        {
            paddle.Move(pointPos.y);
        }
    }
}