using Microsoft.EntityFrameworkCore.Storage;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Infrastructure.Repositories;

namespace OtakuTracker.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OtakutrackerContext _context;
        private IDbContextTransaction _transaction;

        public IAnimeRepository AnimeRepository { get; private set; }
        public IUserRepository UserRepository { get; private set; }
        public IReviewsRepository ReviewRepository { get; private set; }
        public IAnimeListRepository AnimeListRepository { get; private set; }

        public UnitOfWork(OtakutrackerContext context)
        {
            _context = context;
            AnimeRepository = new AnimeRepository(_context);
            UserRepository = new UserRepository(_context);
            ReviewRepository = new ReviewRepository(_context);
            AnimeListRepository = new AnimeListRepository(_context);
        }

        
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            try
            {
                await _transaction.RollbackAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
