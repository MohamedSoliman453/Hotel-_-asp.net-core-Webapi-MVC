namespace Hotel.DataAccess.Interfaces
{
    /// <summary>
    /// Interface for the Unit of Work, coordinating the work of multiple repositories.
    /// </summary>
    public interface IUnitOfWork
    {
        IGenericRepository<T> GenericRepository<T>() where T : class;
        Task SaveAsync();
    }
}
