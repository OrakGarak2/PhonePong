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
        List<EnemyEnum> enemyList;

        public EnemySelector() {}

        public EnemySelector(List<EnemyEnum> enemyList)
        {
            this.enemyList = enemyList;
        }

        public virtual Enemy SelectEnemy()
        {
            return null;
        }
    }

    public class LowRankEnemySelector : EnemySelector
    {
        public LowRankEnemySelector() : base(new List<EnemyEnum> { EnemyEnum.SpaceInvaders, EnemyEnum.PacMan, EnemyEnum.Galaga }) { }
        

    }

    public class MidBossEnemtSelector : EnemySelector
    {
        public MidBossEnemtSelector() : base(new List<EnemyEnum>{EnemyEnum.BreakOut}) {}
    }

    public class HighRankEnemySelector : EnemySelector
    {
        public HighRankEnemySelector() : base(new List<EnemyEnum>{EnemyEnum.Minesweeper, EnemyEnum.Snake, EnemyEnum.PuzzleBobble}) {}
    }

    public class FinalBossEnemySelector : EnemySelector
    {
        public FinalBossEnemySelector() : base(new List<EnemyEnum>(new List<EnemyEnum>{EnemyEnum.Tetris})){}
    }
}
