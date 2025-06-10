// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

// PhonePong
using PhonePong.Layer;

public class GoalLine : MonoBehaviour
{
    private const int ballLayer = LayerDatas.ballLayer;
    [SerializeField] private Score opponentScore;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != ballLayer) return;

        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            bool isMaxScore = opponentScore.IncreaseScore();
            
            if (ball.DontDestroyOnGoal)
            {
                if (isMaxScore) ball.gameObject.SetActive(false);
                else            ball.Reset();
            }
            else
            {
                Destroy(ball.gameObject);
            }
        }
    }
}
