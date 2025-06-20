// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

public class Goalpost : GoalLine
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float movableRange;
    private const float movableRangeOffset = 0.5f;

    [SerializeField] private float sinSeed = 0f;

    [SerializeField] private Transform top;
    [SerializeField] private Transform bottom;

    private void Start()
    {
        moveSpeed = 20f;

        movableRange = (top.position.y - bottom.position.y - transform.localScale.y - movableRangeOffset) * 0.5f;        
    }

    private void Update()
    {
        if (sinSeed > 360f)
        {
            sinSeed = 0f;
        }
        else
        {
            sinSeed += moveSpeed * Time.deltaTime;
        }

        transform.position = new Vector2(transform.position.x, Mathf.Sin(Mathf.Deg2Rad * sinSeed) * movableRange);
    }
}
