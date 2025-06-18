using UnityEngine;

namespace PhonePong.MainMenu
{
    public class MainMenuModel
    {
        public MenuCommand MenuCommand = MenuCommand.None;
        public Orientation Orientation = Orientation.Horizontal;

        public int secretCount { get; private set; }

        public void SecretCountUp()
        {
            secretCount++;
        }
    }
}
