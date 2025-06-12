using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils.Animation;

public class Pause : MonoBehaviour
{
    [SerializeField] private Sprite pauseSprite;
    [SerializeField] private Sprite unpauseSprite;
    [SerializeField] private Button pauseButton;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private TMP_Text countDownText;
    
    private bool isPause;

    private void Awake()
    {
        pauseButton.onClick.AddListener(OnClickPause);

        Init();
    }

    private void Init()
    {
        isPause = false;
        pausePanel.SetActive(false);
        pauseButton.image.sprite = unpauseSprite;
        Time.timeScale = 1;
    }

    private void OnClickPause()
    {
        if (isPause)
        {
            StartCoroutine(CountDownCoroutine());
        }
        else
        {
            isPause = true;
            pausePanel.SetActive(true);
            pauseButton.image.sprite = pauseSprite;
            Time.timeScale = 0;
        }
        
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
        pauseButton.interactable = false;
        pausePanel.SetActive(false);
        
        CountDown("3");
        yield return new WaitForSecondsRealtime(1f);
        CountDown("2");
        yield return new WaitForSecondsRealtime(1f);
        CountDown("1");
        yield return new WaitForSecondsRealtime(1f);
        
        Time.timeScale = 1;
        
        isPause = false;
        pauseButton.interactable = true;
        pauseButton.image.sprite = unpauseSprite;
    }
}
