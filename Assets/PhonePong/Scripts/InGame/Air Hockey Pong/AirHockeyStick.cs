// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

public class AirHockeyStick : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb2D;

    protected virtual void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 movement)
    {
        rb2D.MovePosition(movement);
    }
}
