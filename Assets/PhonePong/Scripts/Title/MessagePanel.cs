using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LegendPingPong.Login
{
    public class MessagePanel : MonoBehaviour
    {
        public TMP_Text messageText;
        public Button checkButton;

        private void OnEnable()
        {
            checkButton.onClick.AddListener(OnClickCheck);
        }

        public void ShowMessage(string message)
        {
            messageText.text = message;
            gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            checkButton.onClick.RemoveAllListeners();
        }

        private void OnClickCheck()
        {
            this.gameObject.SetActive(false);
        }
    }
}

