using Microsoft.EntityFrameworkCore;
using OtakuTracker.Application.Abstractions;
using User = OtakuTracker.Domain.Models.User;

namespace OtakuTracker.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly OtakutrackerContext _context;

    public UserRepository(OtakutrackerContext context)
    {
        _context = context;
    }

    public async Task<User?> CreateUser(User? user)
    {
        user.LastOnline = user.LastOnline?.ToUniversalTime();

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetUserById(int userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> UpdateUser(User? user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteUser(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}