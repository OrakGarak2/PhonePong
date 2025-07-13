// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

namespace PhonePong.VSRetro.Enmity
{
    [Serializable]
    public class EnemyData
    {
        public EnemyEnum enemyEnum;
        public GameObject enemyPrefab;
    }
}