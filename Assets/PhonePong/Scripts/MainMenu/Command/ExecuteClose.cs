using UnityEngine;

namespace PhonePong.MainMenu.Command
{
    public class ExecuteClose : IMenuCommand
    {
        private readonly IMainMenuView view;
        private readonly MainMenuModel model;
        
        public ExecuteClose(IMainMenuView view, MainMenuModel model)
        {
            this.view = view;
            this.model = model;
        }
        
        public void Execute()
        {
            switch (model.MenuCommand)
            {
                case MenuCommand.Start:
                    view.GetSelectPlayersGroup().SetActive(false);
                    view.GetSelectModeGroup().SetActive(false);
                    view.GetPopupPanel().SetActive(false);
                    break;
                case MenuCommand.Settings:
                    view.GetSettingsGroup().SetActive(false);
                    view.GetPopupPanel().SetActive(false);
                    break;
                case MenuCommand.Credit:
                    view.GetCreditGroup().SetActive(false);
                    view.GetPopupPanel().SetActive(false);
                    break;
                default:
                    Debug.LogError("없는 메뉴 타입 입니다.");
                    break;
            }
        }
    }

}
