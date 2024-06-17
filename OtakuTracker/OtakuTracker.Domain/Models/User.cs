namespace OtakuTracker.Domain.Models;

public class User
{
    public int userid { get; set; }
    public string username { get; set; }
    public string email { get; set; }
    public string passwordhash { get; set; }
    public DateTime joindate { get; set; }
    public DateTime lastlogin { get; set; }

}