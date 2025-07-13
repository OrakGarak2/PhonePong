// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

// PhonePong
using PhonePong.VSRetro.Enmity;
using Unity.VisualScripting;

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

        [SerializeField] private Enemy enemy;
        public Enemy Enemy => enemy;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;

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

        public void SpawnEnemy()
        {

        }
    }
}