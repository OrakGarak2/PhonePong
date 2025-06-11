using UnityEngine;

namespace PhonePong.MainMenu.Command
{
    public class ExecuteStart : IMenuCommand
    {
        private readonly IMainMenuView view;
        private readonly MainMenuModel model;

        public ExecuteStart(IMainMenuView view, MainMenuModel model)
        {
            this.view = view;
            this.model = model;
        }
        
        public void Execute()
        {
            model.MenuCommand = MenuCommand.Start;
            
            view.GetPopupPanel().SetActive(true);
            view.GetSelectPlayersGroup().SetActive(true);
        }
    }
}

