using System;
using UnityEngine;
using UnityEngine.UI;

namespace LegendPingPong.MainMenu
{
    public interface IMainMenuView
    {
        // Initialize
        void InitializeCreditPanel();
        void InitializeSettingPanel();
        
        void SetActiveAllPanels(bool isActive);
        void SetActiveAllGroups(bool isActive);
        void OnStartMenu();
        
        void Popup(GameObject obj, Action allCompleted);
        void Closeup(GameObject obj, Action allCompleted);
        void SetParentOfCreditPanel(Developer developer);
        void LoadScene(string sceneName);
        
        // Panel
        GameObject GetTitlePanel();
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
        GameObject GetSingleModeGroup();
        GameObject GetLocalMultiModeGroup();
        GameObject GetSettingsGroup();
        GameObject GetCreditGroup();

        // Toggle
        Toggle GetHorizontalModeToggle();
        Toggle GetVerticalModeToggle();















        void ChangeImageToSecretImage();
    }
}

