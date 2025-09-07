using BlazorApp.Models;
using BlazorApp1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp1.Services
{
    public interface IUserService
    {
        Task<List<Users>> GetUsersAsync();
        Task<Users> GetUserByIdAsync(int id);
        Task CreateUserAsync(Users user);
        Task UpdateUserAsync(Users user);
        Task DeleteUserAsync(int id);
    }
}