using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utils.Animation;

namespace PhonePong.MainMenu
{
    public class MainMenuView : MonoBehaviour, IMainMenuView
    {
        [Header("메뉴 패널")] 
        [SerializeField] private GameObject titlePanel;
        [SerializeField] private GameObject mainPanel;
        [SerializeField] private GameObject popupPanel;
        [SerializeField] private GameObject exitPanel;
        [SerializeField] private Image fadePanel;

        [Header("메뉴 버튼")]
        [SerializeField] private Button startButton;
        [SerializeField] private Button settingButton;
        [SerializeField] private Button creditButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button closeButton;

        [Header("팝업 메뉴")] 
        [SerializeField] private GameObject selectPlayersGroup;
        [SerializeField] private GameObject selectModeGroup;
        [SerializeField] private GameObject settingsGroup;
        [SerializeField] private GameObject creditGroup;
        
        [Header("설정")]
        [SerializeField] private Toggle horizontalModeToggle;
        [SerializeField] private Toggle verticalModeToggle;

        [Header("종료 패널")] 
        [SerializeField] private Button yesButton;
        [SerializeField] private Button noButton;
        
        [Header("플레이어 선택")] 
        [SerializeField] private Button localMultiButton;
        
        [Header("모드 선택")]
        [SerializeField] private Button selectClassicModeButton;
        [SerializeField] private Button selectAbilityModeButton;
        [SerializeField] private Button selectDrawModeButton;

        [Header("크래딧")] 
        [SerializeField] private GameObject creditPanel;
        [SerializeField] private Transform seounghunParent;
        [SerializeField] private Transform junsangParent;
        [SerializeField] private Button seounghunButton;
        [SerializeField] private Button junsangButton;

        [Header("비밀")] 
        [SerializeField] private Button imageButton;
        [SerializeField] private Image normalImage;
        [SerializeField] private Sprite secretImage;

        private MainMenuPresenter presenter;

        private void Awake()
        {
            presenter = new MainMenuPresenter(this, new MainMenuModel());
            
            startButton.onClick.AddListener(() => presenter.Execute(MenuCommand.Start));
            settingButton.onClick.AddListener(() => presenter.Execute(MenuCommand.Settings));
            creditButton.onClick.AddListener(() => presenter.Execute(MenuCommand.Credit));
            exitButton.onClick.AddListener(() => presenter.Execute(MenuCommand.Exit));
            closeButton.onClick.AddListener(() => presenter.Execute(MenuCommand.Close));
            yesButton.onClick.AddListener(() => presenter.Execute(MenuCommand.Yes));
            noButton.onClick.AddListener(() => presenter.Execute(MenuCommand.No));
            localMultiButton.onClick.AddListener(() => presenter.Execute(MenuCommand.LocalMulti));
            selectClassicModeButton.onClick.AddListener(() => presenter.Execute(MenuCommand.SelectClassicMode));
            selectAbilityModeButton.onClick.AddListener(() => presenter.Execute(MenuCommand.SelectAbilityMode));
            selectDrawModeButton.onClick.AddListener(() => presenter.Execute(MenuCommand.SelectDrawMode));
            
            horizontalModeToggle.onValueChanged.AddListener((c) =>
            {
                presenter.ChangeToHorizontalMode();
            });
            verticalModeToggle.onValueChanged.AddListener((c) =>
            {
                presenter.ChangeToVerticalMode();
            });
            
            seounghunButton.onClick.AddListener(() => presenter.OnClickSeounghunImage());
            junsangButton.onClick.AddListener(() => presenter.OnClickJunsangImage());
        }

        #region Initialize

        public void InitializeSettingPanel()
        {
            horizontalModeToggle.isOn = true;
            verticalModeToggle.isOn = false;
        }
        
        public void InitializeCreditPanel()
        {
            creditGroup.SetActive(false);
            creditPanel.transform.localScale = new Vector3(0, 1, 1);
            creditPanel.transform.localPosition = creditGroup.transform.localPosition;
            creditPanel.transform.SetParent(creditGroup.transform);
            creditPanel.SetActive(false);
        }

        #endregion
        
        public void SetActiveAllPanels(bool isActive)
        {
            mainPanel.SetActive(isActive);
            popupPanel.SetActive(isActive);
            exitPanel.SetActive(isActive);
        }

        public void SetActiveAllGroups(bool isActive)
        {
            selectPlayersGroup.SetActive(isActive);
            selectModeGroup.SetActive(isActive);
            settingsGroup.SetActive(isActive);
            creditGroup.SetActive(isActive);
        }

        public void OnStartMenu()
        {
            AnimationUtility.FadeAnimation(this, fadePanel, Color.black, 1f, 0f, 1.5f, 0.5f, null, () =>
            {
                fadePanel.gameObject.SetActive(false);
            });
        }

        public void Popup(GameObject obj, Action allCompleted)
        {
            // x
            AnimationUtility.ScaleAxisAnimation(this, obj, 0, 1f, 0.25f, 0f, () =>
            {
                // y
                AnimationUtility.ScaleAxisAnimation(this, obj, 0, 1f, 0.25f, 0f, null, allCompleted);
            }, null, true);
        }

        public void Closeup(GameObject obj, Action allCompleted)
        {
            // x
            AnimationUtility.ScaleAxisAnimation(this, obj, 1, 0f, 0.25f, 0f, () =>
            {
                // y
                AnimationUtility.ScaleAxisAnimation(this, obj, 1, 0f, 0.25f, 0f, null, allCompleted);
            }, null, true);
        }

        public void SetParentOfCreditPanel(Developer developer)
        {
            creditPanel.transform.localScale = new Vector3(0, 1, 1);
            creditPanel.SetActive(true);
            creditPanel.transform.SetAsLastSibling(); 
            
            switch (developer)
            {
                case Developer.Junsang:
                    junsangParent.SetAsLastSibling();
                    break;
                case Developer.Seounghun:
                    seounghunParent.SetAsLastSibling();
                    break;
            }
            
            AnimationUtility.ScaleAxisAnimation(this, creditPanel, 0, 1f, 0.25f, 0f, null ,ShowSeonghunInfo, true);
        }

        private void ShowSeonghunInfo()
        {
            // AnimationUtility.MoveAnimation(this, (RectTransform)seounghunParent, 0.25f, 0f, new Vector2(-450, -200),
            //     () =>
            //     {
            //         AnimationUtility.ScaleAxisAnimation(this, seounghunParent.gameObject, 1, 1.5f, 0.5f, 0f, () =>
            //         {
            //             AnimationUtility.ScaleAxisAnimation(this, seounghunParent.gameObject, 1, 1.5f, 0.5f, 0f, null ,null, false);
            //         } ,null, true);
            //     }, null);
        }

        public void LoadScene(string sceneName)
        {
            SceneLoader.LoadSceneAsync(this, sceneName);
        }

        public GameObject GetTitlePanel() => titlePanel;
        public GameObject GetMainPanel() => mainPanel;
        public GameObject GetPopupPanel() => popupPanel;
        public GameObject GetExitPanel() => exitPanel;
        
        public Button GetStartButton() => startButton;
        public Button GetSettingsButton() => settingButton;
        public Button GetCreditButton() => creditButton;
        public Button GetExitButton() => exitButton;
        
        public GameObject GetSelectPlayersGroup() => selectPlayersGroup;
        public GameObject GetSelectModeGroup() => selectModeGroup;
        public GameObject GetSettingsGroup() => settingsGroup;
        public GameObject GetCreditGroup() => creditGroup;
        
        public Toggle GetHorizontalModeToggle() => horizontalModeToggle;
        public Toggle GetVerticalModeToggle() => verticalModeToggle;
        
        public void ChangeImageToSecretImage()
        {
            normalImage.sprite = secretImage;
        }
    }
}


