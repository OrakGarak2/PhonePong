using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LegendPingPong.Login
{
    public class RegisterAccount : LoginBase, ILoginView
    {
        [Header("Register")]
        [SerializeField] private TMP_InputField emailInputField;
        [SerializeField] private TMP_InputField idInputField;
        [SerializeField] private TMP_InputField pwInputField;
        [SerializeField] private TMP_InputField rePwInputField;
        [SerializeField] private Button registerButton;
        [SerializeField] private Button backButton;
    
        private RegisterPresenter presenter;
    
        // 이메일 형식에 알맞지 않거나
        // 비밀번호와 비밀번호 확인이 같지 않거나 했을 경우
        // 회원가입 버튼이 활성화 되지 않음

        private void Start()
        {
            presenter = new RegisterPresenter(this, new AuthModel());
        
            emailInputField.onValueChanged.AddListener((_) => { presenter.ValidateRegisterFields(); });
            idInputField.onValueChanged.AddListener((_) => { presenter.ValidateRegisterFields(); });
            pwInputField.onValueChanged.AddListener((_) => { presenter.ValidateRegisterFields(); });
            rePwInputField.onValueChanged.AddListener((_) => { presenter.ValidateRegisterFields(); });
            registerButton.onClick.AddListener(presenter.OnRegisterButtonClicked);
            backButton.onClick.AddListener(presenter.OnBackToLogin);
        }

        public void ShowMessage(string msg) => SetupMessage(msg);
        public void SwitchToLogin() => SwitchTo(LoginType.Login);
        public void SwitchToRegister() => SwitchTo(LoginType.Register);
        public void ClearLoginFields() { }
        public void ClearRegisterFields() => ResetUI(new[] { emailInputField, idInputField, pwInputField, rePwInputField });
        public string GetLoginId() => "";
        public string GetLoginPassword() => "";
        public string GetRegisterEmail() => emailInputField.text;
        public string GetRegisterId() => idInputField.text;
        public string GetRegisterPassword() => pwInputField.text;
        public string GetRegisterPasswordConfirm() => rePwInputField.text;
        public void SetLoginInteractable(bool isActive) { }
        public void SetRegisterInteractable(bool isActive) => registerButton.interactable = isActive;
    }
}

