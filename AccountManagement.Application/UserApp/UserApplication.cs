using _0_FrameWork.FW.Application;
using AccountManagement.Application.Contract.UserCon;
using AccountManagement.Application.Contract.UserCon.ViewModel.Request;
using AccountManagement.Domain.UserAgg;

namespace AccountManagement.Application.UserApp
{
	public class UserApplication : IUserApplication
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthHelper _authHelper;

        public UserApplication(IUserRepository repository, IPasswordHasher passwordHasher, IAuthHelper authHelper)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _authHelper = authHelper;
        }

        public async Task<bool> LoginAsync(User_Login_Request request)
        {

            var User = _repository.GetUserBy(request.Mobile).Result;

            if (User is null)
                return await Task.FromResult(false);

            var password = _passwordHasher.Check(User.PasswordHash, request.Password);

            if (!password.Verified)
                return await Task.FromResult(false);

            _authHelper.SingIn(new AuthViewModel(User.KeyId, User.FullName, User.Mobile));
            return await Task.FromResult(true);
        }


        public async Task<bool> RegisterAsync(User_Register_Request request)
        {
            if (await _repository.ExistsAysenc(x => x.FullName == request.FullName))
                return await Task.FromResult(false);

            var passwordHash = _passwordHasher.Hash(request.Password);

            var user = new User(request.FullName, request.Mobile, passwordHash);

            await _repository.AddAysenc(user);
            await _repository.SaveAsync();

            return await Task.FromResult(true);
        }



    }
}
