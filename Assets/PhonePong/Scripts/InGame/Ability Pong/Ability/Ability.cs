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
    /// <summary>
    /// 능력 사용 메서드
    /// </summary>
    /// <param name="racket">능력을 사용할 라켓</param>
    public abstract void Excute(AbilityRacket racket);
}