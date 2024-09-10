using BehinQueryApp.Model.QueryCommandAgg;
using Microsoft.EntityFrameworkCore;

namespace BehinQueryApp.Db
{
    public class BehinQueryContext : DbContext
    {
        public BehinQueryContext(DbContextOptions<BehinQueryContext> options):base(options)
        {
        }

        public DbSet<QueryCommand> QueryCommands { get; set; }
		public DbSet<QueryCommandLog> QueryCommandLogs { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

    }
}
