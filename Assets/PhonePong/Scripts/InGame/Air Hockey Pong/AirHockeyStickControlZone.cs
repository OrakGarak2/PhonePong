// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

public class AirHockeyStickControlZone : TouchZone
{
    [SerializeField] private AirHockeyStick airHockeyStick;

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (CheckPointInZone(out Vector2 pointPos))
        {
            airHockeyStick.Move(pointPos);
        }
    }
}