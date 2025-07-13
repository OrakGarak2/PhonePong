// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

using PhonePong.VSRetro.Enmity;

namespace PhonePong.VSRetro
{
    public class VSRetroManager : MonoBehaviour
    {
        public static VSRetroManager Instance { get; private set; }

        [Header("적")]
        [SerializeField] private EnemyScriptableObjectScript enemyScriptableObject;
        private EnemySelector enemySelector;

        [Header("스테이지")]
        public int CurrentStage { get; private set; } = 0;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);

                enemySelector = new EnemySelector();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void GoNextStage()
        {
            CurrentStage++;
        }
    }
}