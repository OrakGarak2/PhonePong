// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

namespace PhonePong.VSRetro.Enmity
{
    public class EnemySelector
    {
        private List<EnemyEnum> enemyList;
        private EnemyEnum bossEnemy;

        public EnemySelector() { }

        public EnemySelector(List<EnemyEnum> enemyList, EnemyEnum bossEnemy)
        {
            this.enemyList = enemyList;
            this.bossEnemy = bossEnemy;
        }

        public virtual EnemyEnum SelectEnemy(out bool isBoss)
        {
            if (enemyList.Count != 0)
            {
                EnemyEnum enemy = enemyList[UnityEngine.Random.Range(0, enemyList.Count)];

                enemyList.Remove(enemy);

                isBoss = false;

                return enemy;
            }
            else
            {
                isBoss = true;
                
                return bossEnemy;
            }
        }
    }

    public class LowRankEnemySelector : EnemySelector
    {
        public LowRankEnemySelector() : base(new List<EnemyEnum> { EnemyEnum.SpaceInvaders, EnemyEnum.PacMan, EnemyEnum.Galaga }, EnemyEnum.BreakOut) { }
    }

    public class HighRankEnemySelector : EnemySelector
    {
        public HighRankEnemySelector() : base(new List<EnemyEnum>{EnemyEnum.Minesweeper, EnemyEnum.Snake, EnemyEnum.PuzzleBobble}, EnemyEnum.Tetris) {}
    }
}
