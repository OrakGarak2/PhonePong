using System;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("SFX")] 
    [field: SerializeField] public EventReference ballBounce { get; private set; }
    [field: SerializeField] public EventReference ballVoidBounce { get; private set; }
    [field: SerializeField] public EventReference generateDummyBall { get; private set; }
    [field: SerializeField] public EventReference fireBall { get; private set; }
    [field: SerializeField] public EventReference paddleSizeUp { get; private set; }
    [field: SerializeField] public EventReference goal { get; private set; }
    [field: SerializeField] public EventReference lineDraw { get; private set; }
    [field: SerializeField] public EventReference buttonClick { get; private set; }
    [field: SerializeField] public EventReference music { get; private set; }

    public static FMODEvents Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("FMODEvents instance already exists.");
        }
        Instance = this;   
    }
}
