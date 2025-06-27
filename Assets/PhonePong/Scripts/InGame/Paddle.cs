// System

// Unity
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb2D;

    protected virtual void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void Move(float movement)
    {
        rb2D.MovePosition(new Vector2(rb2D.position.x, movement));
    }
}