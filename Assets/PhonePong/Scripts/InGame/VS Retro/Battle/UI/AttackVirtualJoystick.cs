// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.EventSystems;

namespace PhonePong.VSRetro.Battle.Attack
{
    public class AttackVirtualJoystick : VirtualJoystick
    {
        [SerializeField] private Vector2 standardDirection = Vector2.left;

        private float DotProduct(Vector2 v1, Vector2 v2)
        {
            return (v1.x * v2.x) + (v1.y * v2.y);
        }

        protected override Vector2 GetLeverPos(Vector2 eventPosition, Vector2 joystickPos)
        {
            if (DotProduct(standardDirection, (eventPosition - joystickPos).normalized) > 0f)
            {
                return base.GetLeverPos(eventPosition, joystickPos);
            }
            else
            {
                return base.GetLeverPos(new Vector2(lever.position.x, eventPosition.y), joystickPos);
            }
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            BattleManager.Instance.MainBall.Serve(-leverDirection);

            base.OnEndDrag(eventData);
        }
    }
}