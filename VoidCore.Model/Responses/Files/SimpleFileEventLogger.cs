using VoidCore.Model.DomainEvents;
using VoidCore.Model.Logging;

namespace VoidCore.Model.Responses.Files
{
    /// <inheritdoc/>
    /// <summary>
    /// Log meta information about the item set.
    /// </summary>
    /// <typeparam name="TRequest">The request type</typeparam>
    public class SimpleFileEventLogger<TRequest> : FallibleEventLogger<TRequest, ISimpleFile>
    {
        /// <summary>
        /// Construct a new IItemSet logger.
        /// </summary>
        /// <param name="logger"></param>
        /// <returns></returns>
        public SimpleFileEventLogger(ILoggingService logger) : base(logger) { }

        /// <inheritdoc/>
        public override void OnSuccess(TRequest request, IResult<ISimpleFile> successfulResult)
        {
            Logger.Info(successfulResult.Value.GetLogText());
            base.OnSuccess(request, successfulResult);
        }
    }
}