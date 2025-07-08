// System
using System;

// Unity
using UnityEngine;
using UnityEngine.EventSystems;

namespace PhonePong.VSRetro.Nonbattle
{
    public class MoveVirtualJoystick : VirtualJoystick
    {
        [SerializeField] private NonbattlePaddle nonbattlePaddle;

        [SerializeField] private bool isMove = false;

        [SerializeField] private float paddleMovableMinRange;
        [SerializeField][Range(0f, 1f)] private float paddleMovableMinRangeOffset;

        protected override void Start()
        {
            base.Start();

            if (paddleMovableMinRangeOffset > leverRangeOffset)
            {
                paddleMovableMinRangeOffset = leverRangeOffset;
            }

            paddleMovableMinRange = rectTransform.sizeDelta.x * paddleMovableMinRangeOffset;
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);

            if (PaddleMovable()) isMove = true;
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);

            if (PaddleMovable())
            {
                if (!isMove) isMove = true;
            }
            else if (isMove) isMove = false;
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);

            isMove = false;
        }

        private bool PaddleMovable()
        {
            return ((Vector2)(lever.position - rectTransform.position)).sqrMagnitude >= paddleMovableMinRange * paddleMovableMinRange;
        }

        private void Update()
        {
            if (isMove)
                nonbattlePaddle.Move(leverDirection);
        }
    }
}