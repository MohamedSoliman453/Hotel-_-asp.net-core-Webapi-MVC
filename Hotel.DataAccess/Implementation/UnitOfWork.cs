using Hotel.DataAccess.Interfaces;
using Hotel.DataAccess.Models;
using System;
using System.Threading.Tasks;

namespace Hotel.DataAccess.Implementation
{
    /// <summary>
    /// Implementation of the Unit of Work pattern.
    /// </summary>
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<T> GenericRepository<T>() where T : class
        {
            return new GenericRepository<T>(_context);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
