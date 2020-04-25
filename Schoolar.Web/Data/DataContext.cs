namespace Schoolar.Web.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using Web.Data.Entities;

    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Coordinator> Coordinators { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        /*protected override void OnModelCreating(ModelBuilder builder)
        {
            var cascadeFKs = builder.Model
           .G­etEntityTypes()
           .SelectMany(t => t.GetForeignKeys())
           .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Casca­de);
            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restr­ict;
            }
        }*/
    }
}
