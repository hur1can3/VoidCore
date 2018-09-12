using VoidCore.Model.DomainEvents;
using VoidCore.Model.Logging;

namespace VoidCore.Model.Responses.ItemSet
{
    /// <inheritdoc/>
    /// <summary>
    /// Log meta information about the item set.
    /// </summary>
    /// <typeparam name="TRequest">The request type</typeparam>
    /// <typeparam name="TEntity">The type of items in the item set</typeparam>
    public class ItemSetLogging<TRequest, TEntity> : FallibleLogging<TRequest, IItemSet<TEntity>>
    {
        /// <summary>
        /// Construct a new logger.
        /// </summary>
        /// <param name="logger"></param>
        /// <returns></returns>
        public ItemSetLogging(ILoggingService logger) : base(logger) { }

        /// <inheritdoc/>
        public override void OnSuccess(TRequest request, IResult<IItemSet<TEntity>> successfulResult)
        {
            Logger.Info(successfulResult.Value.GetLogText());
            base.OnSuccess(request, successfulResult);
        }
    }
}