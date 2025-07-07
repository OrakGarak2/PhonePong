// UnityEngine
using UnityEngine;
using UnityEngine.UI;

// Utils
using Utils.Animation;

namespace LegendPingPong.Splash
{
    public class SplashView : MonoBehaviour
    {
        [SerializeField] private Image panel;

        private void Awake()
        {
            Application.targetFrameRate = 120;
        }

        private void Start()
        {
            OnFadeOutPanel();
        }

        private void OnFadeOutPanel()
        {
            AnimationUtility.FadeAnimation(this, panel, Color.black, 1, 0, 3f, 1f, null, OnFadeInPanel);
        }

        private void OnFadeInPanel()
        {
            AnimationUtility.FadeAnimation(this, panel, Color.black, 0, 1, 1.5f, 1f, null, SceneChange);
        }

        private void SceneChange()
        {
            SceneLoader.LoadSceneAsync(this, SceneName.MainMenuScene);
        }
    }
}

