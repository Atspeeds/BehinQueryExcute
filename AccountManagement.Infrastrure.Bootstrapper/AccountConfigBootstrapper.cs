using _0_FrameWork.FW.Application;
using AccountManagement.Application.Contract.UserCon;
using AccountManagement.Application.UserApp;
using AccountManagement.Domain.UserAgg;
using AccountManagement.Infrastrure.UserInfra;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccountManagement.Infrastrure
{
	public class AccountConfigBootstrapper
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserApplication, UserApplication>();

            services.AddTransient<IPasswordHasher, PasswordHasher>();


           
        }
    }
}
