// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

namespace PhonePong.VSRetro.Battle
{
    public enum VSRetroTurn
    {
        Player,
        Enemy
    }

    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance { get; private set; }

        [SerializeField] private GameObject vsRetroBallPrefab;
        public VSRetroBall MainBall { get; private set; }

        [SerializeField] private BattlePaddle battlePaddle;
        public BattlePaddle BattlePaddle => battlePaddle;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                // TODO: 공 생성 코드 작성 -> 작성함.
                SpawnMainBall();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void SpawnMainBall()
        {
            MainBall = Instantiate(vsRetroBallPrefab, vsRetroBallPrefab.transform.position, Quaternion.identity).GetComponent<VSRetroBall>();
            MainBall.SetVSRetroBall(battlePaddle);
        }
    }
}