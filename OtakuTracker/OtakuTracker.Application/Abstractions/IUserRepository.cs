using OtakuTracker.Domain.Models;

namespace OtakuTracker.Application.Abstractions
{
    public interface IUserRepository
    {
        User? CreateUser(User? user);
        User? GetUserById(int userId);
        User? GetUserByUsername(string username);
        List<User?> GetAllUsers();
        User? UpdateUser(User? user);
        void DeleteUser(int userId);
    }
}
