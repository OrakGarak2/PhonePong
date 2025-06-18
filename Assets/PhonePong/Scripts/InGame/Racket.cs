// System

// Unity
using UnityEngine;

public class Racket : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private float speed;

    protected virtual void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void Move(float movement)
    {
        rb2D.MovePosition(new Vector2(rb2D.position.x, movement));
    }
}