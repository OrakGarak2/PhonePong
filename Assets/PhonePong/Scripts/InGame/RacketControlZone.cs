// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

public class RacketControlZone : TouchZone
{
    [SerializeField] private Racket racket;

    private void Update()
    {
        if (CheckPointInZone(out Vector2 pointPos))
        {
            racket.Move(pointPos.y);
        }
    }
}