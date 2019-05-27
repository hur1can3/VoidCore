
using VoidCore.AspNet.Data;
using VoidCore.Model.Logging;

namespace VoidCore.Efcore.UnitOfWork
{
    /// <summary>
    /// Defines the interfaces for <see cref="IEfRepositoryFactory{TEntity}"/> interfaces.
    /// </summary>
    public interface IEfRepositoryFactory
    {

        /// <summary>
        /// Gets the specified writable repository for the <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>An instance of type <see cref="EfWritableRepository{TEntity}"/> repository.</returns>
        EfWritableRepository<TEntity> GetWritableRepository<TEntity>(ILoggingStrategy loggingStrategy, bool hasCustomRepository = false) where TEntity : class;

        /// <summary>
        /// Gets the specified read only repository for the <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>An instance of type <see cref="EfReadOnlyRepository{TEntity}"/> repository.</returns>
        EfReadOnlyRepository<TEntity> GetReadOnlyRepository<TEntity>(ILoggingStrategy loggingStrategy, bool hasCustomRepository = false) where TEntity : class;

    }
}
