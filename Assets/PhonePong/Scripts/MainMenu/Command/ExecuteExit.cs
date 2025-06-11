using UnityEngine;

namespace PhonePong.MainMenu.Command
{
    public class ExecuteExit : IMenuCommand
    {
        private readonly IMainMenuView view;
        private readonly MainMenuModel model;

        public ExecuteExit(IMainMenuView view, MainMenuModel model)
        {
            this.view = view;
            this.model = model;
        }
        
        public void Execute()
        {
            model.MenuCommand = MenuCommand.Exit;
            
            view.GetExitPanel().SetActive(true);
        }
    }
}

