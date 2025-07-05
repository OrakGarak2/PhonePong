namespace LegendPingPong.Login
{
    public interface ILoginView 
    {
        void ShowMessage(string message);
        void SwitchToLogin();
        void SwitchToRegister();
        void ClearLoginFields();
        void ClearRegisterFields();

        string GetLoginId();
        string GetLoginPassword();

        string GetRegisterEmail();
        string GetRegisterId();
        string GetRegisterPassword();
        string GetRegisterPasswordConfirm();
    
        void SetLoginInteractable(bool isActive);
        void SetRegisterInteractable(bool isActive);
    }
}

