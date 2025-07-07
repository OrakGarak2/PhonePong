using BackEnd;
using FMOD;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

namespace LegendPingPong.Login
{
    public class LoginPresenter
    {
        private readonly ILoginView view;
        private readonly AuthModel model;

        public LoginPresenter(ILoginView view, AuthModel model)
        {
            this.view = view;
            this.model = model;
        
            ValidateLoginFields();
        }

        public void OnLoginButtonClicked()
        {
            string id = view.GetLoginId();
            string pw = view.GetLoginPassword();
        
            model.Login(id, pw, (success, msg) =>
            {
                if (success)
                {
                    view.ClearLoginFields();
                    SceneManager.LoadScene(SceneName.MainMenuScene);
                }
                else
                {
                    view.ShowMessage(msg);
                }
            });
        }

        public void OnRegisterButtonClicked()
        {
            view.ClearLoginFields();
            view.SwitchToRegister();
        }
        
        public void ValidateLoginFields()
        {
            bool isValid = !string.IsNullOrWhiteSpace(view.GetLoginId()) && 
                           !string.IsNullOrWhiteSpace(view.GetLoginPassword());
            view.SetLoginInteractable(isValid);
        }

        public void OnGuestLoginButtonClicked()
        {
            BackendReturnObject bro = Backend.BMember.GuestLogin("게스트 로그인으로 로그인함");
            if (bro.IsSuccess())
            {
                SceneManager.LoadScene(SceneName.MainMenuScene);
                Debug.Log("게스트 로그인에 성공했습니다");
            }
        }
    }
}

