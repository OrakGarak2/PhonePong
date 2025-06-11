using UnityEngine;
using UnityEngine.UI;

namespace PhonePong.MainMenu
{
    public interface IMainMenuView
    {
        void SetActiveAllPanels(bool isActive);
        void OnStartMenu();
        
        // Panel
        GameObject GetMainPanel();
        GameObject GetPopupPanel();
        GameObject GetExitPanel();
        
        // Button
        Button GetStartButton();
        Button GetSettingsButton();
        Button GetCreditButton();
        Button GetExitButton();
        
        // Group
        GameObject GetSelectPlayersGroup();
        GameObject GetSelectModeGroup();
        GameObject GetSettingsGroup();
        GameObject GetCreditGroup();
    }
}

