// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

// PhonePong
using PhonePong.VSRetro.Enmity;

namespace PhonePong.VSRetro.Battle.Skill
{
    [Serializable]
    public class SkillData
    {
        public Sprite sprite;
        
        public string name;
        [TextArea] public string description;
        
        public ISkill skill;
    }
}