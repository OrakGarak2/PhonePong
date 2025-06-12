using System;
using UnityEngine;
using UnityEngine.UI;

namespace PhonePong.MainMenu
{
    public interface IMainMenuView
    {
        // Initialize
        void InitializeCreditPanel();
        
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
        GameObject GetSettingsGroup();
        GameObject GetCreditGroup();





















        void ChangeImageToSecretImage();
    }
}

