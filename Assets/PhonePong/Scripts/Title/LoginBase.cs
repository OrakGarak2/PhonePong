using TMPro;
using UnityEngine;

namespace LegendPingPong.Login
{
    public class LoginBase : MonoBehaviour
    {
        protected enum LoginType
        {
            Login = 0, Register,
        }
    
        [Header("Panel")]
        [SerializeField] private GameObject loginPanel;
        [SerializeField] private GameObject registerPanel;
        [SerializeField] private MessagePanel messagePanel;

        private void Awake()
        {
            SwitchTo(LoginType.Login);
            messagePanel.gameObject.SetActive(false);
        }

        protected static void ResetUI(TMP_InputField[] inputFields)
        {
            foreach (var inputField in inputFields)
            {
                inputField.text = string.Empty;
            }
        }

        /// <summary>
        /// 패널 변경
        /// </summary>
        /// <param name="loginType">로그인 타입</param>
        protected void SwitchTo(LoginType loginType)
        {
            loginPanel.SetActive(loginType == LoginType.Login);
            registerPanel.SetActive(loginType == LoginType.Register);
        }

        protected void SetupMessage(string message)
        {
            messagePanel.ShowMessage(message);
        }
    }
}

