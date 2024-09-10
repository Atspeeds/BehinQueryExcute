using _0_FrameWork.FW.Infrastrure;
using AccountManagement.Domain.UserAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastrure.UserInfra
{
	public class UserRepository : RepositoryBase<long, User>, IUserRepository
    {
        private readonly AccountContext _Context;

        public UserRepository(AccountContext context) : base(context)
        {
            _Context = context;
        }

        public Task<User> GetUserBy(string phone)
        {
            return _Context.Users.FirstOrDefaultAsync(x => x.Mobile == phone);
        }
    }
}
