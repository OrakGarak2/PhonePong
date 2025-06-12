using System;
using UnityEngine;

namespace PhonePong.MainMenu.Command
{
    public class DelegateCommand : IMenuCommand
    {
        private readonly Action action;

        public DelegateCommand(Action action)
        {
            this.action = action;
        }

        public void Execute()
        {
            action?.Invoke();
        }
    }

}
