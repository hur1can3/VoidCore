using System.Linq;
using VoidCore.Model.DomainEvents;

namespace VoidCore.Model.Logging
{
    /// <summary>
    /// A base post processor that logs IFallible failures to a string logger.
    /// </summary>
    /// <typeparam name="TRequest">The request type of the event.</typeparam>
    /// <typeparam name="TResponse">The response type of the event.</typeparam>
    public class FallibleLoggingPostProcessor<TRequest, TResponse> : PostProcessorAbstract<TRequest, TResponse>
    {
        /// <summary>
        /// Construct a new post processor
        /// </summary>
        /// <param name="logger">The ILoggingService to log to</param>
        public FallibleLoggingPostProcessor(ILoggingService logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// Log failures of the IFallible. If other you are overriding this method, be sure to call base() to
        /// invoke this default behavior.
        /// </summary>
        /// <param name="request">The domain event request</param>
        /// <param name="result">The result of the event, this contains the response if successful</param>
        public override void OnFailure(TRequest request, IFallible result)
        {
            Logger.Warn("Failures: " + string.Join(" ", result.Failures.Select(x => x.Message)));
            base.OnFailure(request, result);
        }

        /// <summary>
        /// Instance of a logging service.
        /// </summary>
        protected readonly ILoggingService Logger;
    }
}
