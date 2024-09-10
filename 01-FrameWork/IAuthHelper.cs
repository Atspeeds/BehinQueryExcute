namespace _0_FrameWork.FW.Application
{
    public interface IAuthHelper
    {
        void SingIn(AuthViewModel account);
        void SignOut();
        AuthViewModel GetCurrentUserInfo();
        long GetUserId();
    }
}
