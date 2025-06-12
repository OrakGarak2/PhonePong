using UnityEngine;

namespace PhonePong.MainMenu
{
    public class MainMenuModel
    {
        public MenuCommand MenuCommand = MenuCommand.None;

        public int secretCount { get; private set; }

        public void SecretCountUp()
        {
            secretCount++;
        }
    }
}
