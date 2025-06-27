// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

// PhonePong
using PhonePong.Enum;

public abstract class Ability : MonoBehaviour
{
    public float cooldown;

    /// <summary>
    /// 능력 사용 메서드
    /// </summary>
    /// <param name="paddle">능력을 사용할 패들</param>
    public abstract void Excute(AbilityPaddle paddle);
}