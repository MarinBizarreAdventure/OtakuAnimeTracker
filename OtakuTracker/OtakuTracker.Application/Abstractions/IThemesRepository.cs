using OtakuTracker.Domain.Models;


namespace OtakuTracker.Application.Abstractions
{
    public interface IThemesRepository
    {
        Theme CreateThemes(Theme themes);
        Theme GetThemesById(int themesId);
        List<Theme> GetAllThemes();
        Theme UpdateThemes(Theme themes);
        void DeleteThemes(int themesId);
    }
}
