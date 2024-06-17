namespace OtakuTracker.Domain.Models;

public class Theme
{
    public int ThemeId { get; set; }
    public string ThemeName { get; set; }
    public List<Anime> Animes { get; set; }


}