using OtakuTracker.Domain.Models;

namespace OtakuTracker.Application.Abstractions;

public interface IWatchingStatusRepository
{
    Task<IEnumerable<WatchingStatus>> GetAll();
    Task<WatchingStatus> GetById(int id);
    Task Add(WatchingStatus watchingStatus);
    Task Update(WatchingStatus watchingStatus);
    Task Delete(int id);
}