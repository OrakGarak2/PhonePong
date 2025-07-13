using System.Collections;
using System.Collections.Generic;
using LegendPingPong;
using LegendPingPong.MainMenu;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils.Animation;

public class PauseManager : MonoBehaviour
{
    [Header("UI 구성요소")] 
    [SerializeField] private RectTransform p1ScoreTextRect;
    [SerializeField] private RectTransform p2ScoreTextRect;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject pausePanel;

    [Header("카운트 다운")] 
    [SerializeField] private GameObject countDownTextPrefab;
    [SerializeField] private Transform countDownParent;
    [SerializeField] private List<TMP_Text> countDownTexts = new ();
    [SerializeField] private string[] countDownTextGroup = {"1", "2", "3"};

    private void Awake()
    {
        pauseButton.onClick.AddListener(Pause);
        restartButton.onClick.AddListener(Restart);
        exitButton.onClick.AddListener(Exit);
        
        Setup();
        
        Init();
    }

    private void Init()
    {
        pausePanel.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        pausePanel.gameObject.SetActive(false);

        foreach (var text in countDownTexts)
        {
            text.gameObject.SetActive(false);
        }
        
        Time.timeScale = 1;
    }

    private void Setup()
    {
        RectTransform pauseButtonRect = pauseButton.GetComponent<RectTransform>();
        RectTransform restartButtonRect = restartButton.GetComponent<RectTransform>();
        RectTransform exitButtonRect = exitButton.GetComponent<RectTransform>();
        
        RectTransform p1Text = Instantiate(countDownTextPrefab, countDownParent).gameObject.GetComponent<RectTransform>();
        countDownTexts.Add(p1Text.GetComponent<TMP_Text>());
        
        switch (GameManager.Instance.orientation)
        {
            case Orientation.Horizontal:
                p1ScoreTextRect.anchorMin = new Vector2(0.5f, 1f);
                p1ScoreTextRect.anchorMax = new Vector2(0.5f, 1f);
                p1ScoreTextRect.pivot = new Vector2(0.5f, 0.5f);
                p1ScoreTextRect.anchoredPosition = new Vector2(440, -150);
                p1ScoreTextRect.rotation = Quaternion.Euler(0, 0, 0);
                
                p2ScoreTextRect.anchorMin = new Vector2(0.5f, 1f);
                p2ScoreTextRect.anchorMax = new Vector2(0.5f, 1f);
                p2ScoreTextRect.pivot = new Vector2(0.5f, 0.5f);
                p2ScoreTextRect.anchoredPosition = new Vector2(-440, -150);
                p2ScoreTextRect.rotation = Quaternion.Euler(0, 0, 0);
                
                pauseButtonRect.anchorMin = new Vector2(0.5f, 1f);
                pauseButtonRect.anchorMax = new Vector2(0.5f, 1f);
                pauseButtonRect.pivot = new Vector2(0.5f, 1f);
                pauseButtonRect.anchoredPosition = new Vector2(0f, -90f);
                
                restartButtonRect.anchorMin = new Vector2(0.5f, 1f);
                restartButtonRect.anchorMax = new Vector2(0.5f, 1f);
                restartButtonRect.pivot = new Vector2(0.5f, 1f);
                restartButtonRect.anchoredPosition = new Vector2(-100f, -90f);
                restartButtonRect.rotation = Quaternion.Euler(0f, 0f, 0f);
                
                exitButtonRect.anchorMin = new Vector2(0.5f, 1f);
                exitButtonRect.anchorMax = new Vector2(0.5f, 1f);
                exitButtonRect.pivot = new Vector2(0.5f, 1f);
                exitButtonRect.anchoredPosition = new Vector2(100f, -90f);
                restartButtonRect.rotation = Quaternion.Euler(0f, 0f, 0f);
                
                p1Text.anchorMin = new Vector2(0f, 0f);
                p1Text.anchorMax = new Vector2(1f, 1f);
                p1Text.pivot = new Vector2(0.5f, 0.5f);
                p1Text.offsetMin = new Vector2(-250f, 0f);
                p1Text.offsetMax = new Vector2(250f, 0f);
                p1Text.gameObject.GetComponent<TMP_Text>().fontSize = 100;
                
                break;
            case Orientation.Vertical:
                p1ScoreTextRect.anchorMin = new Vector2(0.5f, 0.5f);
                p1ScoreTextRect.anchorMax = new Vector2(0.5f, 0.5f);
                p1ScoreTextRect.pivot = new Vector2(0.5f, 0.5f);
                p1ScoreTextRect.anchoredPosition = new Vector2(135, -35);
                p1ScoreTextRect.rotation = Quaternion.Euler(0, 0, 90);
                
                p2ScoreTextRect.anchorMin = new Vector2(0.5f, 0.5f);
                p2ScoreTextRect.anchorMax = new Vector2(0.5f, 0.5f);
                p2ScoreTextRect.pivot = new Vector2(0.5f, 0.5f);
                p2ScoreTextRect.anchoredPosition = new Vector2(-135, 35);
                p2ScoreTextRect.rotation = Quaternion.Euler(0, 0, -90);
                
                pauseButtonRect.anchorMin = new Vector2(0.5f, 1f);
                pauseButtonRect.anchorMax = new Vector2(0.5f, 1f);
                pauseButtonRect.pivot = new Vector2(0.5f, 1f);
                pauseButtonRect.anchoredPosition = new Vector2(0f, -90f);
                
                restartButtonRect.anchorMin = new Vector2(0.5f, 0.5f);
                restartButtonRect.anchorMax = new Vector2(0.5f, 0.5f);
                restartButtonRect.pivot = new Vector2(0.5f, 0.5f);
                restartButtonRect.anchoredPosition = new Vector2(0f, 100f);
                restartButtonRect.rotation = Quaternion.Euler(0f, 0f, -90f);
                
                exitButtonRect.anchorMin = new Vector2(0.5f, 0.5f);
                exitButtonRect.anchorMax = new Vector2(0.5f, 0.5f);
                exitButtonRect.pivot = new Vector2(0.5f, 0.5f);
                exitButtonRect.anchoredPosition = new Vector2(0f, -100f);
                exitButtonRect.rotation = Quaternion.Euler(0f, 0f, -90f);
                
                RectTransform p2Text = Instantiate(countDownTextPrefab, countDownParent).gameObject.GetComponent<RectTransform>();
                countDownTexts.Add(p2Text.GetComponent<TMP_Text>());
                
                p1Text.anchorMin = new Vector2(0.5f, 0.5f);
                p1Text.anchorMax = new Vector2(0.5f, 0.5f);
                p1Text.pivot = new Vector2(0.5f, 0.5f);
                p1Text.anchoredPosition = new Vector2(400f, 35f);
                p1Text.sizeDelta = new Vector2(500f, 500f);
                p1Text.rotation = Quaternion.Euler(0f, 0f, 90f);
                
                p1Text.gameObject.GetComponent<TMP_Text>().fontSize = 100;
                
                p2Text.anchorMin = new Vector2(0.5f, 0.5f);
                p2Text.anchorMax = new Vector2(0.5f, 0.5f);
                p2Text.pivot = new Vector2(0.5f, 0.5f);
                p2Text.anchoredPosition = new Vector2(-400f, -35f);
                p2Text.sizeDelta = new Vector2(500f, 500f);
                p2Text.rotation = Quaternion.Euler(0f, 0f, -90f);
                
                p2Text.gameObject.GetComponent<TMP_Text>().fontSize = 100;
                break;
        }
    }

    private void Pause()
    {
        pauseButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        pausePanel.gameObject.SetActive(true);
        
        foreach (var text in countDownTexts)
        {
            text.gameObject.SetActive(true);
        }

        foreach (var text in countDownTexts)
        {
            text.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            text.color = new Color(1, 1, 1, 1);
            text.text = "Pause";
        }
        
        Time.timeScale = 0;
    }

    private void Restart()
    {
        StopAllCoroutines();
        StartCoroutine(CountDownCoroutine());
    }

    private void Exit()
    {
        Time.timeScale = 1;
        AudioManager.Instance.StopMusic();
        SceneManager.LoadScene(SceneName.MainMenuScene);
    }

    private void CountDown(TMP_Text countDownText, string text)
    {
        countDownText.text = text;
        
        AnimationUtility.FadeAnimation(this, countDownText, Color.white, 0, 1, 0.5f, 0, () =>
        {
            AnimationUtility.ScaleAxisAnimation(this, countDownText.gameObject, 0, 1, 0.5f, 0, () =>
            {
                AnimationUtility.ScaleAxisAnimation(this, countDownText.gameObject, 0, 1, 0.5f, 0, null, null);
            }, null, true);
        }, () =>
        {
            AnimationUtility.FadeAnimation(this, countDownText, Color.white, 1, 0, 0.5f, 0, () =>
            {
                AnimationUtility.ScaleAxisAnimation(this, countDownText.gameObject, 1, 0, 0.5f, 0, () =>
                {
                    AnimationUtility.ScaleAxisAnimation(this, countDownText.gameObject, 1, 0, 0.5f, 0, null, null);
                }, null, true);
            }, null);
        });
    }

    private IEnumerator CountDownCoroutine()
    {
        restartButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        pausePanel.gameObject.SetActive(false);

        int count = countDownTextGroup.Length - 1;

        while (count >= 0)
        {
            foreach (var t in countDownTexts)
            {
                CountDown(t, countDownTextGroup[count]);
            }
            yield return new WaitForSecondsRealtime(1f);
            count--;
        }
        
        pauseButton.gameObject.SetActive(true);
        
        foreach (var text in countDownTexts)
        {
            text.gameObject.SetActive(false);
        }
        
        Time.timeScale = 1;
    }
}
