using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Models;

namespace UserManagement.Services.Domain.Interfaces;

public interface IUserService 
{
    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    IEnumerable<User> FilterByActive(bool isActive);
    IEnumerable<User> GetAll();

    void Add(User user);

    User? GetById(long id);

    void Update(User user);

    void Delete(long id);

    Task<User?> GetByIdAsync(long id);
    Task<IEnumerable<User>> GetAllAsync();
    Task<IEnumerable<User>> FilterByActiveAsync(bool isActive);
    Task DeleteAsync(long id);
    Task UpdateAsync(User user);
    Task AddAsync(User user);

}
