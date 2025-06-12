// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private RectTransform win;
    [SerializeField] private int currentScore;
    const int maxScore = 15;

    private void Start()
    {
        scoreText = GetComponent<Text>();
        rectTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// Score를 업데이트하고 최대 스코어에 도달했으면 true, 아니면 false를 반환한다.
    /// </summary>
    /// <returns>최대 스코어 달성 여부</returns>
    public bool IncreaseScore()
    {
        currentScore++;
        scoreText.text = currentScore.ToString();

        if (currentScore >= maxScore)
        {
            // Game Over
            win.position = new Vector2(rectTransform.position.x, win.position.y);
            win.gameObject.SetActive(true);

            return true;
        }

        return false;
    }
}
