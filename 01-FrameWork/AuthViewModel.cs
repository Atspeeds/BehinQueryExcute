namespace _0_FrameWork.FW.Application
{
    public class AuthViewModel
    {
        public long Id { get; set; }
        public string FullName { get;  set; }
        public string Mobile { get;  set; }

        public AuthViewModel(long id, string fullName, string mobile)
        {
            Id = id;
            FullName = fullName;
            Mobile = mobile;
        }

        public AuthViewModel()
        {
        }
    }
}
