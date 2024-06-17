using OtakuTracker.Domain.Models;

namespace OtakuTracker.Application.Users.Responses
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime LastLogin { get; set; }
        public string PasswordHash { get; set; }

        public static UserDto FromUser(User? user)
        {
            return new UserDto
            {
                UserId = user.userid,
                Username = user.username,
                PasswordHash = user.passwordhash,
                Email = user.email,
                JoinDate = user.joindate,
                LastLogin = user.lastlogin
            };
        }

    }

    
}
