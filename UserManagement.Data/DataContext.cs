using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.Data;

public class DataContext : DbContext, IDataContext
{
    public DataContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseInMemoryDatabase("UserManagement.Data.DataContext");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Forename = "Peter", Surname = "Loew", Email = "ploew@example.com", IsActive = true, DateOfBirth = new DateTime(1996, 8, 20) },
            new User { Id = 2, Forename = "Benjamin Franklin", Surname = "Gates", Email = "bfgates@example.com", IsActive = true, DateOfBirth = new DateTime(1985, 11, 3) },
            new User { Id = 3, Forename = "Castor", Surname = "Troy", Email = "ctroy@example.com", IsActive = false, DateOfBirth = new DateTime(1989, 5, 12) },
            new User { Id = 4, Forename = "Memphis", Surname = "Raines", Email = "mraines@example.com", IsActive = true, DateOfBirth = new DateTime(1986, 9, 25) },
            new User { Id = 5, Forename = "Stanley", Surname = "Goodspeed", Email = "sgodspeed@example.com", IsActive = true, DateOfBirth = new DateTime(1992, 3, 8) },
            new User { Id = 6, Forename = "H.I.", Surname = "McDunnough", Email = "himcdunnough@example.com", IsActive = true, DateOfBirth = new DateTime(1987, 10, 17) },
            new User { Id = 7, Forename = "Cameron", Surname = "Poe", Email = "cpoe@example.com", IsActive = false, DateOfBirth = new DateTime(1995, 2, 14) },
            new User { Id = 8, Forename = "Edward", Surname = "Malus", Email = "emalus@example.com", IsActive = false, DateOfBirth = new DateTime(1990, 7, 9) },
            new User { Id = 9, Forename = "Damon", Surname = "Macready", Email = "dmacready@example.com", IsActive = false, DateOfBirth = new DateTime(1993, 4, 28) },
            new User { Id = 10, Forename = "Johnny", Surname = "Blaze", Email = "jblaze@example.com", IsActive = true, DateOfBirth = new DateTime(1988, 12, 30) },
            new User { Id = 11, Forename = "Robin", Surname = "Feld", Email = "rfeld@example.com", IsActive = true, DateOfBirth = new DateTime(1997, 6, 19) }

        );

        modelBuilder.Entity<Log>().HasData(
            new Log { Id = 1, UserId = 1, Time = new DateTime(2024, 1, 1, 8, 0, 0), ActionTaken = LogActionType.Create, Forename = "Peter", Surname = "Loew", Email = "ploew@example.com", IsActive = true, DateOfBirth = new DateTime(1996, 8, 20) },
            new Log { Id = 2, UserId = 2, Time = new DateTime(2024, 1, 1, 8, 0, 0), ActionTaken = LogActionType.Create, Forename = "Benjamin Franklin", Surname = "Gates", Email = "bfgates@example.com", IsActive = true, DateOfBirth = new DateTime(1985, 11, 3) },
            new Log { Id = 3, UserId = 3, Time = new DateTime(2024, 1, 1, 8, 0, 0), ActionTaken = LogActionType.Create, Forename = "Castor", Surname = "Troy", Email = "ctroy@example.com", IsActive = false, DateOfBirth = new DateTime(1989, 5, 12) },
            new Log { Id = 4, UserId = 4, Time = new DateTime(2024, 1, 1, 8, 0, 0), ActionTaken = LogActionType.Create, Forename = "Memphis", Surname = "Raines", Email = "mraines@example.com", IsActive = true, DateOfBirth = new DateTime(1986, 9, 25) },
            new Log { Id = 5, UserId = 5, Time = new DateTime(2024, 1, 1, 8, 0, 0), ActionTaken = LogActionType.Create, Forename = "Stanley", Surname = "Goodspeed", Email = "sgodspeed@example.com", IsActive = true, DateOfBirth = new DateTime(1992, 3, 8) },
            new Log { Id = 6, UserId = 6, Time = new DateTime(2024, 1, 1, 8, 0, 0), ActionTaken = LogActionType.Create, Forename = "H.I.", Surname = "McDunnough", Email = "himcdunnough@example.com", IsActive = true, DateOfBirth = new DateTime(1987, 10, 17) },
            new Log { Id = 7, UserId = 7, Time = new DateTime(2024, 1, 1, 8, 0, 0), ActionTaken = LogActionType.Create, Forename = "Cameron", Surname = "Poe", Email = "cpoe@example.com", IsActive = false, DateOfBirth = new DateTime(1995, 2, 14) },
            new Log { Id = 8, UserId = 8, Time = new DateTime(2024, 1, 1, 8, 0, 0), ActionTaken = LogActionType.Create, Forename = "Edward", Surname = "Malus", Email = "emalus@example.com", IsActive = false, DateOfBirth = new DateTime(1990, 7, 9) },
            new Log { Id = 9, UserId = 9, Time = new DateTime(2024, 1, 1, 8, 0, 0), ActionTaken = LogActionType.Create, Forename = "Damon", Surname = "Macready", Email = "dmacready@example.com", IsActive = false, DateOfBirth = new DateTime(1993, 4, 28) },
            new Log { Id = 10, UserId = 10, Time = new DateTime(2024, 1, 1, 8, 0, 0), ActionTaken = LogActionType.Create, Forename = "Johnny", Surname = "Blaze", Email = "jblaze@example.com", IsActive = true, DateOfBirth = new DateTime(1988, 12, 30) },
            new Log { Id = 11, UserId = 11, Time = new DateTime(2024, 1, 1, 8, 0, 0), ActionTaken = LogActionType.Create, Forename = "Robin", Surname = "Feld", Email = "rfeld@example.com", IsActive = true, DateOfBirth = new DateTime(1997, 6, 19) }
        );
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Log> Logs { get; set; }

    public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        => base.Set<TEntity>();

    public void Create<TEntity>(TEntity entity) where TEntity : class
    {
        base.Add(entity);
        SaveChanges();
    }

    public new void Update<TEntity>(TEntity entity) where TEntity : class
    {
        base.Update(entity);
        SaveChanges();
    }

    public void Delete<TEntity>(TEntity entity) where TEntity : class
    {
        base.Remove(entity);
        SaveChanges();
    }
    public async Task<List<TEntity>> GetAllAsync<TEntity>() where TEntity : class
        => await base.Set<TEntity>().ToListAsync();

    public async Task CreateAsync<TEntity>(TEntity entity) where TEntity : class
    {
        await base.AddAsync(entity);
        await SaveChangesAsync();
    }

    public async Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class
    {
        base.Update(entity);
        await SaveChangesAsync();
    }

    public async Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class
    {
        base.Remove(entity);
        await SaveChangesAsync();
    }

}
