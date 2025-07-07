using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LegendPingPong.Login
{
    public class Login : LoginBase, ILoginView
    {
        [Header("Login")]
        [SerializeField] private TMP_InputField idInputField;
        [SerializeField] private TMP_InputField pwInputField;
        [SerializeField] private Button loginButton;
        [SerializeField] private Button registerButton;
        [SerializeField] private Button findPwButton;
        [SerializeField] private Button backButton;

        [SerializeField] private Button guestLoginButton;
        
    
        private LoginPresenter presenter;

        private void Start()
        {
            presenter = new LoginPresenter(this, new AuthModel());

            idInputField.onValueChanged.AddListener((_) => presenter.ValidateLoginFields());
            pwInputField.onValueChanged.AddListener((_) => presenter.ValidateLoginFields());
            loginButton.onClick.AddListener(presenter.OnLoginButtonClicked);
            registerButton.onClick.AddListener(presenter.OnRegisterButtonClicked);
            findPwButton.onClick.AddListener(() => { });
            
            guestLoginButton.onClick.AddListener(presenter.OnGuestLoginButtonClicked);
        }

        public void ShowMessage(string msg) => SetupMessage(msg);
        public void SwitchToLogin() => SwitchTo(LoginType.Login);
        public void SwitchToRegister() => SwitchTo(LoginType.Register);
        public void ClearLoginFields() => ResetUI(new[] { idInputField, pwInputField });
        public void ClearRegisterFields() { }
        public string GetLoginId() => idInputField.text;
        public string GetLoginPassword() => pwInputField.text;
        public string GetRegisterEmail() => "";
        public string GetRegisterId() => "";
        public string GetRegisterPassword() => "";
        public string GetRegisterPasswordConfirm() => "";
        public void SetLoginInteractable(bool isActive) => loginButton.interactable = isActive;
        public void SetRegisterInteractable(bool isActive) { }
    }
}

