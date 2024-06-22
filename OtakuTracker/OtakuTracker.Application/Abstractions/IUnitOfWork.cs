namespace OtakuTracker.Application.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        IAnimeRepository AnimeRepository { get; }
        IUserRepository UserRepository { get; }

        IReviewsRepository ReviewRepository { get; }
        IAnimeListRepository AnimeListRepository { get; }
        IGenresRepository GenresRepository { get; }
        IAnimeGenreRepository AnimeGenreRepository { get; }


        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task<int> CompleteAsync();
       
    }

}
