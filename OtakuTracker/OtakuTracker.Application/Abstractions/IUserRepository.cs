using OtakuTracker.Domain.Models;

namespace OtakuTracker.Application.Abstractions
{
    public interface IUserRepository
    {
        Task<User?> CreateUser(User? user);
        Task<User?> GetUserById(int userId);
        Task<User?> GetUserByUsername(string username);
        // Task<List<User?>> GetAllUsers();
        Task<User?> UpdateUser(User? user);
        Task DeleteUser(int userId);
    }
}
