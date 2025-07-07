using UnityEngine;
using BackEnd;
using TMPro;
using UnityEngine.UI;

public class BackendManager : MonoBehaviour
{
    public Button hashButton;
    public TMP_InputField getHashKeyText;
    
    private void Awake()
    {
        var bro = Backend.Initialize();

        if (bro.IsSuccess())
        {
            Debug.Log($"초기화 성공 : {bro}");
        }
        else
        {
            Debug.LogError($"초기화 실패 : {bro}");
        }
        hashButton.onClick.AddListener(GetGoogleHashKey);
        
    }

    public void GetGoogleHashKey()
    {
        string googleHash = Backend.Utils.GetGoogleHash();

        Debug.Log(googleHash);
        if (getHashKeyText != null)
        {
            getHashKeyText.text = googleHash;
        }
    }
}
