using UnityEngine.SceneManagement;

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
    }
}

