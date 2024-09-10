using _01_FrameWork;

namespace AccountManagement.Domain.UserAgg
{
	public interface IUserRepository : IRepositoryBase<long, User>
    {
        Task<User> GetUserBy(string phone);
    }
}
