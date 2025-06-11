using UnityEngine;

namespace PhonePong.MainMenu.Command
{
    public class ExecuteCredit : IMenuCommand
    {
        private readonly IMainMenuView view;
        private readonly MainMenuModel model;

        public ExecuteCredit(IMainMenuView view, MainMenuModel model)
        {
            this.view = view;
            this.model = model;
        }
        
        public void Execute()
        {
            model.MenuCommand = MenuCommand.Credit;
            
            view.GetPopupPanel().SetActive(true);
            view.GetCreditGroup().SetActive(true);
        }
    }
}

