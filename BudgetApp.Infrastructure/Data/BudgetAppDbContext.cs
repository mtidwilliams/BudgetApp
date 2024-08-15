using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Infrastructure.Data;
public class BudgetAppDbContext : DbContext, IDatabaseService
{
    public BudgetAppDbContext(DbContextOptions<BudgetAppDbContext> options) : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Address> Addresses { get; set; }
    // public virtual DbSet<UserType> UserTypes { get; set; }
    // public virtual DbSet<Notification> Notifications { get; set; }
    public virtual DbSet<Person> Persons { get; set; }
    // public virtual DbSet<Permission> Permissions { get; set; }
    // public virtual DbSet<UserPasswordHistory> UserPasswordHistories { get; set; }

    // public virtual DbSet<UserPreferences> UserPreferences { get; set; }

    // public virtual DbSet<Setting> Settings { get; set; }
}
