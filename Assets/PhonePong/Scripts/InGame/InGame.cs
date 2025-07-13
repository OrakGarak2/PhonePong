using UnityEngine;

public class InGame : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.InitializeMusic(FMODEvents.Instance.inGame);
    }
}
