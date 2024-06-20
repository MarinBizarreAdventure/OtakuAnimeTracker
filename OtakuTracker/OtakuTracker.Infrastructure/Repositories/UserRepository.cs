// using OtakuTracker.Application.Abstractions;
// using OtakuTracker.Infrastructure.Models;
// using User = OtakuTracker.Domain.Models.User;
//
// namespace OtakuTracker.Infrastructure.Repositories;
//
// public class UserRepository : IUserRepository
// {
//     private readonly OtakutrackerContext _context;
//
//     public UserRepository(OtakutrackerContext context)
//     {
//         _context = context;
//     }
//
//     public User? CreateUser(User? user)
//     {
//         _context.users.Add(user);
//         _context.SaveChanges();
//         return user;
//     }
//
//     public User? GetUserById(int userId)
//     {
//         return _context.users.Find(userId);
//     }
//
//     public User? GetUserByUsername(string username)
//     {
//         return _context.users.FirstOrDefault(u => u != null && u.username == username);
//     }
//
//     public List<User?> GetAllUsers()
//     {
//         return _context.users.ToList();
//     }
//
//     public User? UpdateUser(User? user)
//     {
//         _context.users.Update(user);
//         _context.SaveChanges();
//         return user;
//     }
//
//     public void DeleteUser(int userId)
//     {
//         var user = _context.users.Find(userId);
//         if (user == null) return;
//         _context.users.Remove(user);
//         _context.SaveChanges();
//     }
// }