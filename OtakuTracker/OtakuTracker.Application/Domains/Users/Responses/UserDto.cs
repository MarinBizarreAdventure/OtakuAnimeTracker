using OtakuTracker.Domain.Models;

namespace OtakuTracker.Application.Users.Responses
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public string? Email { get; set; }

        public int? UserWatching { get; set; }

        public int? UserCompleted { get; set; }

        public int? UserOnhold { get; set; }

        public int? UserDropped { get; set; }

        public int? UserPlantowatch { get; set; }

        public double? UserDaysSpentWatching { get; set; }

        public string? Gender { get; set; }

        public string? Location { get; set; }

        public DateOnly? BirthDate { get; set; }

        public int? AccessRank { get; set; }

        public DateTime? JoinDate { get; set; }

        public DateTime? LastOnline { get; set; }

        public double? StatsMeanScore { get; set; }

        public int? StatsRewatched { get; set; }

        public int? StatsEpisodes { get; set; }

        public static UserDto FromUser(User? user)
        {
            if (user == null)
                return null;

            return new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                UserWatching = user.UserWatching,
                UserCompleted = user.UserCompleted,
                UserOnhold = user.UserOnhold,
                UserDropped = user.UserDropped,
                UserPlantowatch = user.UserPlantowatch,
                UserDaysSpentWatching = user.UserDaysSpentWatching,
                Gender = user.Gender,
                Location = user.Location,
                BirthDate = user.BirthDate,
                AccessRank = user.AccessRank,
                JoinDate = user.JoinDate,
                LastOnline = user.LastOnline,
                StatsMeanScore = user.StatsMeanScore,
                StatsRewatched = user.StatsRewatched,
                StatsEpisodes = user.StatsEpisodes
            };
        }
    }


    
}
