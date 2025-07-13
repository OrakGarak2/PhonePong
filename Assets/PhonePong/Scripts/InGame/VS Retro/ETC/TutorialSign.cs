// System
using System;
using System.Collections;
using System.Collections.Generic;
using PhonePong.VSRetro.Nonbattle;


// Unity
using UnityEngine;

public class TutorialSign : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("튜토리얼");
    }
}