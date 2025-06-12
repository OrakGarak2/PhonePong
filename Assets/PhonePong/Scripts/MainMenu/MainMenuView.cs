using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Utils.Animation;

namespace PhonePong.MainMenu
{
    public class MainMenuView : MonoBehaviour, IMainMenuView
    {
        [Header("메뉴 패널")]
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

        [Header("종료 패널")] 
        [SerializeField] private Button yesButton;
        [SerializeField] private Button noButton;

        [Header("플레이어 선택")] 
        [SerializeField] private Button selectPlayer1Button;
        [SerializeField] private Button selectPlayer2Button;
        
        [Header("모드 선택")]
        [SerializeField] private Button selectClassicModeButton;
        [SerializeField] private Button selectAbilityModeButton;

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
            selectPlayer1Button.onClick.AddListener(() => presenter.Execute(MenuCommand.Select1P));
            selectPlayer2Button.onClick.AddListener(() => presenter.Execute(MenuCommand.Select2P));
            selectClassicModeButton.onClick.AddListener(() => presenter.Execute(MenuCommand.SelectClassicMode));
            selectAbilityModeButton.onClick.AddListener(() => presenter.Execute(MenuCommand.SelectAbilityMode));
        }

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
    }
}


