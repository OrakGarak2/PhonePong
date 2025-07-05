// System
using System.Collections.Generic;

// UnityEngine
using PhonePong.MainMenu.Command;
using UnityEngine;

namespace LegendPingPong.MainMenu
{
    public enum MenuCommand
    {
        None,
        Start,
        Settings,
        Credit,
        Exit,
        Close,
        Yes,
        No,
        Single,
        VsRetroMode,
        SurvivalMode,
        LocalMulti,
        SelectClassicMode,
        SelectAbilityMode,
        SelectDrawMode,
        SelectAirHockeyMode
    }

    public enum Orientation
    {
        Horizontal,
        Vertical
    }

    public enum Developer
    {
        Junsang,
        Seounghun
    }
    
    public class MainMenuPresenter
    {
        private readonly IMainMenuView view;
        private readonly MainMenuModel model;
        
        private readonly Dictionary<MenuCommand, IMenuCommand> commands;

        public MainMenuPresenter(IMainMenuView view, MainMenuModel model)
        {
            this.view = view;
            this.model = model;
            this.commands = new Dictionary<MenuCommand, IMenuCommand>();
            
            Initialize();
        }
        
        private void Initialize()
        {
            // Command
            commands[MenuCommand.Start] = new DelegateCommand(StartCommand);
            commands[MenuCommand.Settings] = new DelegateCommand(SettingsCommand);
            commands[MenuCommand.Credit] = new DelegateCommand(CreditCommand);
            commands[MenuCommand.Exit] = new DelegateCommand(ExitCommand);
            commands[MenuCommand.Close] = new DelegateCommand(CloseCommand);
            commands[MenuCommand.Yes] = new DelegateCommand(YesCommand);
            commands[MenuCommand.No] = new DelegateCommand(NoCommand);
            commands[MenuCommand.Single] = new DelegateCommand(SelectSingleModeCommand);
            commands[MenuCommand.LocalMulti] = new DelegateCommand(SelectLocalMultiModeCommand);
            commands[MenuCommand.SelectClassicMode] = new DelegateCommand(SelectClassicModeGroup);
            commands[MenuCommand.SelectAbilityMode] = new DelegateCommand(SelectAbilityModeGroup);
            commands[MenuCommand.SelectDrawMode] = new DelegateCommand(SelectDrawModeGroup);
            commands[MenuCommand.SelectAirHockeyMode] = new DelegateCommand(SelectAirHockeyModeGroup);
            
            // Setting
            view.SetActiveAllPanels(false);
            view.SetActiveAllGroups(false);
            view.GetMainPanel().SetActive(true);
            view.InitializeCreditPanel();
            view.InitializeSettingPanel();
            
            // FadeOut
            view.OnStartMenu();
        }
        
        public void Execute(MenuCommand command)
        {
            if (commands.TryGetValue(command, out var commandResult))
            {
                commandResult.Execute();
            }
        }
        public void OnClickSeounghunImage()
        {
            view.SetParentOfCreditPanel(Developer.Seounghun);
            
            model.SecretCountUp();

            if (model.secretCount >= 5)
            {
                view.ChangeImageToSecretImage();
            }
        }

        public void OnClickJunsangImage()
        {
            view.SetParentOfCreditPanel(Developer.Junsang);
        }

        public void ChangeToHorizontalMode()
        {
            var h = view.GetHorizontalModeToggle();
            var v = view.GetVerticalModeToggle();
            
            h.SetIsOnWithoutNotify(true);
            v.SetIsOnWithoutNotify(false);
            
            model.Orientation = Orientation.Horizontal;
            GameManager.Instance.orientation = Orientation.Horizontal;
        }
        
        public void ChangeToVerticalMode()
        {
            var h = view.GetHorizontalModeToggle();
            var v = view.GetVerticalModeToggle();
            
            h.SetIsOnWithoutNotify(false);
            v.SetIsOnWithoutNotify(true);
            
            model.Orientation = Orientation.Vertical;
            GameManager.Instance.orientation = Orientation.Vertical;
        }


        #region Commands

        private void StartCommand()
        {
            model.MenuCommand = MenuCommand.Start;
            
            view.GetPopupPanel().SetActive(true);
            view.GetSelectPlayersGroup().SetActive(true);
                
            view.Popup(view.GetPopupPanel(), null);
        }

        private void SettingsCommand()
        {
            model.MenuCommand = MenuCommand.Settings;
            
            view.GetPopupPanel().SetActive(true);
            view.GetSettingsGroup().SetActive(true);
            
            view.Popup(view.GetPopupPanel(), null);
        }

        private void CreditCommand()
        {
            model.MenuCommand = MenuCommand.Credit;
            
            view.GetPopupPanel().SetActive(true);
            view.GetCreditGroup().SetActive(true);
            
            view.Popup(view.GetPopupPanel(), null);
        }

        private void ExitCommand()
        {
            model.MenuCommand = MenuCommand.Exit;
            
            view.GetExitPanel().SetActive(true);
            
            view.Popup(view.GetExitPanel(), null);
        }

        private void CloseCommand()
        {
            view.Closeup(view.GetPopupPanel(), () =>
            {
                switch (model.MenuCommand)
                {
                    case MenuCommand.Start:
                        model.MenuCommand = MenuCommand.None;
                        view.GetSelectPlayersGroup().SetActive(false);
                        view.GetSelectModeGroup().SetActive(false);
                        view.GetPopupPanel().SetActive(false);
                        break;
                    case MenuCommand.Settings:
                        model.MenuCommand = MenuCommand.None;
                        view.GetSettingsGroup().SetActive(false);
                        view.GetPopupPanel().SetActive(false);
                        break;
                    case MenuCommand.Credit:
                        model.MenuCommand = MenuCommand.None;
                        view.InitializeCreditPanel();
                        view.GetCreditGroup().SetActive(false);
                        view.GetPopupPanel().SetActive(false);
                        break;
                    default:
                        Debug.LogError("없는 메뉴 타입 입니다.");
                        break;
                }
            });
        }

        private void YesCommand()
        {
            Application.Quit();
        }

        private void NoCommand()
        {
            model.MenuCommand = MenuCommand.None;
            
            view.Closeup(view.GetExitPanel(), () =>
            {
                view.GetExitPanel().SetActive(false);
            });
        }

        private void SelectSingleModeCommand()
        {
            // Single 버튼을 눌렀을 때 SingleGroup을 Active
            view.GetPopupPanel().SetActive(true);
            view.GetSelectPlayersGroup().SetActive(false); view.GetSelectModeGroup().SetActive(true);
            view.GetSingleModeGroup().SetActive(true); view.GetLocalMultiModeGroup().SetActive(false);
            
        }
        
        private void SelectLocalMultiModeCommand()
        {
            // LocalMulti 버튼을 눌렀을 때 LocalMultiGroup을 Active
            view.GetPopupPanel().SetActive(true);
            view.GetSelectPlayersGroup().SetActive(false); view.GetSelectModeGroup().SetActive(true);
            view.GetSingleModeGroup().SetActive(false); view.GetLocalMultiModeGroup().SetActive(true);
        }

        private void SelectClassicModeGroup()
        {
            // 로딩창 -> 클래식 모드 씬으로 이동
            view.LoadScene(SceneName.ClassicModeScene);
        }

        private void SelectAbilityModeGroup()
        {
            // 로딩창 -> 능력자 모드 씬으로 이동
            view.LoadScene(SceneName.AbilityModeScene);
        }

        private void SelectDrawModeGroup()
        {
            // 로딩창 -> 그리기 모드 씬으로 이동
            view.LoadScene(SceneName.DrawModeScene);
        }
        
        private void SelectAirHockeyModeGroup()
        {
            // 로딩창 -> 에어하키 모드 씬으로 이동
            view.LoadScene(SceneName.AirHockeyModeScene);
        }

        #endregion

    }
}

