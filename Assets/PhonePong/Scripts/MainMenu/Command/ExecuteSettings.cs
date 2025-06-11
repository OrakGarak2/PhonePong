using UnityEngine;

namespace PhonePong.MainMenu.Command
{
    public class ExecuteSettings : IMenuCommand
    {
        private readonly IMainMenuView view;
        private readonly MainMenuModel model;

        public ExecuteSettings(IMainMenuView view, MainMenuModel model)
        {
            this.view = view;
            this.model = model;
        }
        
        public void Execute()
        {
            model.MenuCommand = MenuCommand.Settings;
            
            view.GetPopupPanel().SetActive(true);
            view.GetSettingsGroup().SetActive(true);
        }
    }
}

