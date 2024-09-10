using AccountManagement.Application.Contract.UserCon.ViewModel.Request;
using System.Threading.Tasks;

namespace AccountManagement.Application.Contract.UserCon
{
    public interface IUserApplication
    {
        Task<bool> RegisterAsync(User_Register_Request request);
        Task<bool> LoginAsync(User_Login_Request request);
    }
}
