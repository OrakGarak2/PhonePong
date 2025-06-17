using System.Collections;
using PhonePong;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils.Animation;

public class PauseManager : MonoBehaviour
{
    [Header("UI 구성요소")]
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject pausePanel;
    
    [Header("카운트 다운")]
    [SerializeField] private TMP_Text countDownText;
    [SerializeField] private string[] countDownTextGroup = new [] {"1", "2", "3"};

    private void Awake()
    {
        pauseButton.onClick.AddListener(Pause);
        restartButton.onClick.AddListener(Restart);
        exitButton.onClick.AddListener(Exit);
        Init();
    }

    private void Init()
    {
        pausePanel.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        pausePanel.gameObject.SetActive(false);
        
        Time.timeScale = 1;
    }

    private void Pause()
    {
        pauseButton.gameObject.SetActive(false);
        pausePanel.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        pausePanel.gameObject.SetActive(true);
        
        Time.timeScale = 0;
    }

    private void Restart()
    {
        StartCoroutine(CountDownCoroutine());
    }

    private void Exit()
    {
        SceneLoader.LoadSceneAsync(this, SceneName.MainScene, () =>
        {
            Time.timeScale = 1;
        });
    }

    private void CountDown(string text)
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
            CountDown(countDownTextGroup[count]);
            yield return new WaitForSecondsRealtime(1f);
            count--;
        }
        
        pauseButton.gameObject.SetActive(true);
        Time.timeScale = 1;
    }
}
