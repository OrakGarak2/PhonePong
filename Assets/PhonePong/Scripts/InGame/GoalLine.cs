// System
using System;
using System.Collections;
using System.Collections.Generic;
using LegendPingPong;

// Unity
using UnityEngine;

// PhonePong
using PhonePong.Layer;
using UnityEngine.SceneManagement;

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
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.goal, ball.transform.position);
            bool isMaxScore = opponentScore.IncreaseScore();
            
            if (ball.DontDestroyOnGoal)
            {
                if (isMaxScore)
                {
                    StartCoroutine(ExitGame());
                    ball.gameObject.SetActive(false);
                }
                else
                    ball.Reset();
            }
            else
            {
                Destroy(ball.gameObject);
            }
        }
    }

    private IEnumerator ExitGame()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneName.MainMenuScene);
    }
}
