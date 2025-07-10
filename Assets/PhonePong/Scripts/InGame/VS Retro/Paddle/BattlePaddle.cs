// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;


namespace PhonePong.VSRetro.Battle
{
    public class BattlePaddle : Paddle
    {
        private Action onMove;

        public void OnMoveRegisterHandler(Action action)
        {
            onMove += action;
        }

        public void OnMoveUnregisterHandler(Action action)
        {
            onMove -= action;
        }

        public override void Move(float movement)
        {
            base.Move(movement);

            onMove?.Invoke();
        }
    }
}