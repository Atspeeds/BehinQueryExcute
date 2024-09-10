using AccountManagement.Domain.UserAgg;
using AccountManagement.Infrastrure.UserInfra;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastrure
{
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {
        }

        #region TableAccount
        public DbSet<User> Users { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assamble = typeof(UserConfiguration).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assamble);
            base.OnModelCreating(modelBuilder);
        }



    }
}
