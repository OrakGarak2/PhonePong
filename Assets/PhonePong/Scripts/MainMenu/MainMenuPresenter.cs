// System
using System.Collections.Generic;

// UnityEngine
using PhonePong.MainMenu.Command;
using UnityEngine;

namespace PhonePong.MainMenu
{
    public enum MenuCommand
    {
        None,
        Start,
        Settings,
        Credit,
        Exit,
        Close
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
            
            var executeStart = new ExecuteStart(view, model);
            var executeSettings = new ExecuteSettings(view, model);
            var executeCredit = new ExecuteCredit(view, model);
            var executeExit = new ExecuteExit(view, model);
            var executeClose = new ExecuteClose(view, model);

            var commandDic = new Dictionary<MenuCommand, IMenuCommand>
            {
                {MenuCommand.Start, executeStart},
                {MenuCommand.Settings, executeSettings},
                {MenuCommand.Credit, executeCredit},
                {MenuCommand.Exit, executeExit},
                {MenuCommand.Close, executeClose}
            };
            
            commands = commandDic;
        
            Initialize();
        }
        
        private void Initialize()
        {
            // Setting
            view.SetActiveAllPanels(false);
            view.GetMainPanel().SetActive(true);
            
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
    }
}

