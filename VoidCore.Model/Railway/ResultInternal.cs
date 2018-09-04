using System;
using System.Linq;

namespace VoidCore.Model.Railway
{
    /// <summary>
    /// This class holds the internal logic for the Result abstract class and its inheritors. InternalResult should not be directly accessed outside of
    /// its Result wrapper. Inspired by https://github.com/vkhorikov/CSharpFunctionalExtensions
    /// </summary>
    internal sealed class ResultInternal
    {
        public IFailure[] Failures { get; } = new IFailure[0];

        public bool IsFailed { get; }
        public bool IsSuccess => !IsFailed;

        internal ResultInternal(bool isFailure, IFailure[] failures)
        {
            if (isFailure)
            {
                if (failures == null)
                {
                    throw new ArgumentNullException(nameof(failures), "Failures must not be null for a failed result.");
                }

                if (!failures.Any())
                {
                    throw new ArgumentException("Failures must be provided for failed result.", nameof(failures));
                }

                Failures = failures;
            }
            else if (failures != null)
            {
                throw new ArgumentException("Cannot provide failures for success result.", nameof(failures));
            }

            IsFailed = isFailure;
        }
    }
}