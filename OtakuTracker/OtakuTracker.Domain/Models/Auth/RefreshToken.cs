namespace OtakuTracker.Domain.Models.Auth;

public class RefreshToken
{
    public string Token { get; set; }
    public DateTime ExpiryDate { get; set; }
    public string UserId { get; set; }
    public bool IsRevoked { get; set; }
    public bool IsUsed { get; set; }
}