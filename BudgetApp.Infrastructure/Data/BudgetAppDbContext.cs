using Microsoft.EntityFrameworkCore;
using BudgetApp.Core.Common.Interfaces;
using BudgetApp.Domain.Entities;

namespace BudgetApp.Infrastructure.Data;
public class BudgetAppDbContext : DbContext, IDatabaseService
{
    public BudgetAppDbContext(DbContextOptions<BudgetAppDbContext> options) : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Address> Addresses { get; set; }
    public virtual DbSet<Person> Persons { get; set; }
    public virtual DbSet<Budget> Budgets { get; set; }
    public virtual DbSet<Expense> Expenses { get; set; }
    public virtual DbSet<IncomeSource> IncomeSources { get; set; }
    // public virtual DbSet<UserType> UserTypes { get; set; }
    // public virtual DbSet<Notification> Notifications { get; set; }
    // public virtual DbSet<Permission> Permissions { get; set; }
    // public virtual DbSet<UserPasswordHistory> UserPasswordHistories { get; set; }

    // public virtual DbSet<UserPreferences> UserPreferences { get; set; }

    // public virtual DbSet<Setting> Settings { get; set; }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {

        return await base.SaveChangesAsync(cancellationToken);
    }
}
