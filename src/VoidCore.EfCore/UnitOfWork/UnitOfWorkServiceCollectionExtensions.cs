
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VoidCore.Model.Data;

namespace VoidCore.Efcore.UnitOfWork
{
    /// <summary>
    /// Extension methods for setting up unit of work related services in an <see cref="IServiceCollection"/>.
    /// </summary>
    public static class EfUnitOfWorkServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the unit of work given context as a service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TContext">The type of the db context.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        /// <remarks>
        /// This method only support one db context, if been called more than once, will throw exception.
        /// </remarks>
        public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services) where TContext : DbContext
        {
            services.AddScoped<IEfRepositoryFactory, EfUnitOfWork<TContext>>();
            // Following has a issue: IEfUnitOfWork cannot support multiple dbcontext/database,
            // that means cannot call AddUnitOfWork<TContext> multiple times.
            // Solution: check IEfUnitOfWork whether or null
            services.AddScoped<IEfUnitOfWork, EfUnitOfWork<TContext>>();
            services.AddScoped<IEfUnitOfWork<TContext>, EfUnitOfWork<TContext>>();

            return services;
        }

        /// <summary>
        /// Registers the unit of work given context as a service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TContext1">The type of the db context.</typeparam>
        /// <typeparam name="TContext2">The type of the db context.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        /// <remarks>
        /// This method only support one db context, if been called more than once, will throw exception.
        /// </remarks>
        public static IServiceCollection AddUnitOfWork<TContext1, TContext2>(this IServiceCollection services)
            where TContext1 : DbContext
            where TContext2 : DbContext
        {
            services.AddScoped<IEfUnitOfWork<TContext1>, EfUnitOfWork<TContext1>>();
            services.AddScoped<IEfUnitOfWork<TContext2>, EfUnitOfWork<TContext2>>();

            return services;
        }

        /// <summary>
        /// Registers the unit of work given context as a service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TContext1">The type of the db context.</typeparam>
        /// <typeparam name="TContext2">The type of the db context.</typeparam>
        /// <typeparam name="TContext3">The type of the db context.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        /// <remarks>
        /// This method only support one db context, if been called more than once, will throw exception.
        /// </remarks>
        public static IServiceCollection AddUnitOfWork<TContext1, TContext2, TContext3>(this IServiceCollection services)
            where TContext1 : DbContext
            where TContext2 : DbContext
            where TContext3 : DbContext
        {
            services.AddScoped<IEfUnitOfWork<TContext1>, EfUnitOfWork<TContext1>>();
            services.AddScoped<IEfUnitOfWork<TContext2>, EfUnitOfWork<TContext2>>();
            services.AddScoped<IEfUnitOfWork<TContext3>, EfUnitOfWork<TContext3>>();

            return services;
        }

        /// <summary>
        /// Registers the unit of work given context as a service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TContext1">The type of the db context.</typeparam>
        /// <typeparam name="TContext2">The type of the db context.</typeparam>
        /// <typeparam name="TContext3">The type of the db context.</typeparam>
        /// <typeparam name="TContext4">The type of the db context.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        /// <remarks>
        /// This method only support one db context, if been called more than once, will throw exception.
        /// </remarks>
        public static IServiceCollection AddUnitOfWork<TContext1, TContext2, TContext3, TContext4>(this IServiceCollection services)
            where TContext1 : DbContext
            where TContext2 : DbContext
            where TContext3 : DbContext
            where TContext4 : DbContext
        {
            services.AddScoped<IEfUnitOfWork<TContext1>, EfUnitOfWork<TContext1>>();
            services.AddScoped<IEfUnitOfWork<TContext2>, EfUnitOfWork<TContext2>>();
            services.AddScoped<IEfUnitOfWork<TContext3>, EfUnitOfWork<TContext3>>();
            services.AddScoped<IEfUnitOfWork<TContext4>, EfUnitOfWork<TContext4>>();

            return services;
        }

        /// <summary>
        /// Registers the custom repository as a service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TRepository">The type of the custom repositry.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddCustomWriteRepository<TEntity, TRepository>(this IServiceCollection services)
            where TEntity : class
            where TRepository : class, IWritableRepository<TEntity>
        {
            services.AddScoped<IWritableRepository<TEntity>, TRepository>();

            return services;
        }

        /// <summary>
        /// Registers the custom repository as a service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TRepository">The type of the custom repositry.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddCustomReadRepository<TEntity, TRepository>(this IServiceCollection services)
            where TEntity : class
            where TRepository : class, IReadOnlyRepository<TEntity>
        {
            services.AddScoped<IReadOnlyRepository<TEntity>, TRepository>();

            return services;
        }
    }
}
