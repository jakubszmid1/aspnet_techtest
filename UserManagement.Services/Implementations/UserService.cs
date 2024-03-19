using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class UserService : IUserService
{
    private readonly IDataContext _dataAccess;
    public UserService(IDataContext dataAccess) => _dataAccess = dataAccess;

    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    public IEnumerable<User> FilterByActive(bool isActive) => (
        _dataAccess.GetAll<User>()
        .Where(x => x.IsActive == isActive)
        );

    public IEnumerable<User> GetAll() => _dataAccess.GetAll<User>();

    public void Add(User user) => _dataAccess.Create(user);

    public User? GetById(long id) => (
        _dataAccess.GetAll<User>()
        .FirstOrDefault(u => u.Id == id)
        );

    public void Update(User user) => _dataAccess.Update<User>(user);

    public void Delete(long id)
    {
        User user = GetById(id) ?? throw new InvalidOperationException("User not found");
        _dataAccess.Delete<User>(user);
    }

    public async Task<IEnumerable<User>> FilterByActiveAsync(bool isActive)
    {
        return (await _dataAccess.GetAllAsync<User>().ConfigureAwait(false))
            .Where(x => x.IsActive == isActive);
    }

    public async Task<IEnumerable<User>> GetAllAsync() => await _dataAccess.GetAllAsync<User>();

    public async Task AddAsync(User user)
    {
        await _dataAccess.CreateAsync(user).ConfigureAwait(false);
    }

    public async Task<User?> GetByIdAsync(long id)
    {
        return (await _dataAccess.GetAllAsync<User>().ConfigureAwait(false))
            .FirstOrDefault(u => u.Id == id);
    }

    public async Task UpdateAsync(User user)
    {
        await _dataAccess.UpdateAsync(user).ConfigureAwait(false);
    }

    public async Task DeleteAsync(long id)
    {
        var user = await GetByIdAsync(id).ConfigureAwait(false);
        if (user == null)
        {
            throw new InvalidOperationException("User not found");
        }
        await _dataAccess.DeleteAsync(user).ConfigureAwait(false);
    }
}
