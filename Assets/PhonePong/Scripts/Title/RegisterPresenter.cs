using UnityEngine;

namespace LegendPingPong.Login
{
    public class RegisterPresenter
    {   
        private readonly ILoginView view;
        private readonly AuthModel model;

        public RegisterPresenter(ILoginView view, AuthModel model)
        {
            this.view = view;
            this.model = model;
        
            ValidateRegisterFields();
        }

        public void OnRegisterButtonClicked()
        {
            string email = view.GetRegisterEmail();
            string id = view.GetRegisterId();
            string pw = view.GetRegisterPassword();
            string confirmPw = view.GetRegisterPasswordConfirm();

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(id) ||
                string.IsNullOrWhiteSpace(pw) || string.IsNullOrWhiteSpace(confirmPw)
                || pw != confirmPw || !AuthModel.IsValidEmail(email))
            {
                view.SetRegisterInteractable(false);
                return;
            }
        
            model.Register(id, pw, email, (success, msg) =>
            {
                if (success)
                {
                    view.ClearRegisterFields();
                    view.SwitchToLogin();
                }
                else
                {
                    view.ShowMessage(msg);
                }
            });
        }

        public void OnBackToLogin()
        {
            view.ClearRegisterFields();
            view.SwitchToLogin();
        }

        public void ValidateRegisterFields()
        {
            string email = view.GetRegisterEmail();
            string id = view.GetRegisterId();
            string pw = view.GetRegisterPassword();
            string confirmPw = view.GetRegisterPasswordConfirm();
        
            bool valid = !string.IsNullOrWhiteSpace(email) && 
                         !string.IsNullOrWhiteSpace(id) && 
                         !string.IsNullOrWhiteSpace(pw) && 
                         !string.IsNullOrWhiteSpace(confirmPw) &&
                         pw == confirmPw &&
                         AuthModel.IsValidEmail(email);
        
            view.SetRegisterInteractable(valid);
        }
    }
}

