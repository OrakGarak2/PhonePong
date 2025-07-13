// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

namespace PhonePong.VSRetro.Battle
{
    public class VSRetroBall : MonoBehaviour
    {
        [Header("물리")]
        [SerializeField] private Rigidbody2D rb2D;

        [Header("패들, 공의 위치")]
        [SerializeField] private BattlePaddle battlePaddle;
        [SerializeField] private Vector2 resetPosOffset = new Vector2(0.5f, 0f);

        [Header("속력")]
        [SerializeField] private float speed = 10f;

        private void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
        }

        private void OnDestroy()
        {
            battlePaddle.OnMoveUnregisterHandler(SetPos);
        }

        public void SetVSRetroBall(BattlePaddle paddle)
        {
            battlePaddle = paddle;
            
            Reset();
        }

        public void Reset()
        {
            SetPos();
            battlePaddle.OnMoveRegisterHandler(SetPos);
        }

        private void SetPos()
        {
            transform.position = (Vector2)battlePaddle.transform.position + resetPosOffset;
        }

        public void Serve(Vector2 direction)
        {
            battlePaddle.OnMoveUnregisterHandler(SetPos);
            rb2D.linearVelocity = direction * speed;
        }
    }
}