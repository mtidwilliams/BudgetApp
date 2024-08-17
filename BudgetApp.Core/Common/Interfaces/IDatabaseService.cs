
using Microsoft.EntityFrameworkCore;
using BudgetApp.Domain.Entities;

namespace BudgetApp.Core.Common.Interfaces;

public interface IDatabaseService
{
    DbSet<User> Users { get; set; }
    DbSet<Address> Addresses { get; set; }
    DbSet<Person> Persons { get; set; }
    // DbSet<UserType> UserTypes { get; set; }
    // DbSet<Notification> Notifications { get; set; }
    // DbSet<Permission> Permissions { get; set; }
    // DbSet<UserPasswordHistory> UserPasswordHistories { get; set; }
    // DbSet<UserPreferences> UserPreferences { get; set; }
    // DbSet<Setting> Settings { get; set; }
}
