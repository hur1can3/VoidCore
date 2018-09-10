﻿using System.Threading.Tasks;

namespace VoidCore.Model.DomainEvents
{
    /// <summary>
    /// An event in the domain that takes a request to return a response.
    /// The event request can be validated before handling.
    /// The event can also be fallible itself, returning a Result of response.
    /// The event can be appended with post processors for concerns like logging.
    /// </summary>
    /// <typeparam name="TRequest">The type of the event request</typeparam>
    /// <typeparam name="TResponse">The type of the event response</typeparam>
    public interface IDomainEvent<in TRequest, TResponse>
    {
        /// <summary>
        /// Handle the domain event. This includes validating the request, handling the event, and running any post processors.
        /// </summary>
        /// <param name="request">The request contains all the parameters to handle the event.</param>
        /// <returns>A result of the response.</returns>
        Task<Result<TResponse>> Handle(TRequest request);
    }
}