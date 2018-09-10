﻿using System;
using System.Collections.Generic;
using System.Linq;
using VoidCore.Model.DomainEvents;

namespace VoidCore.Model.Validation
{
    /// <summary>
    /// A rule for validating an entity.
    /// </summary>
    public class Rule<TValidatableEntity> : IRule<TValidatableEntity>, IRuleBuilder<TValidatableEntity>
    {
        private readonly IFailure _failureToThrowWhenViolated;
        private readonly List<Func<TValidatableEntity, bool>> _invalidConditions = new List<Func<TValidatableEntity, bool>>();
        private readonly List<Func<TValidatableEntity, bool>> _suppressConditions = new List<Func<TValidatableEntity, bool>>();

        /// <summary>
        /// Construct a new rule and underlying validation error to throw when violations are detected.
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="uiHandle"></param>
        private Rule(string errorMessage, string uiHandle)
        {
            _failureToThrowWhenViolated = new Failure(errorMessage, uiHandle);
        }

        /// <inheritdoc/>
        public Result Validate(TValidatableEntity validatable)
        {
            if (_suppressConditions.Any() && _suppressConditions.All(c => c.Invoke(validatable)))
            {
                return Result.Ok();
            }

            if (_invalidConditions.Any() && _invalidConditions.All(c => c.Invoke(validatable)))
            {
                return Result.Fail(_failureToThrowWhenViolated);
            }

            return Result.Ok();
        }

        /// <inheritdoc/>
        public IRuleBuilder<TValidatableEntity> InvalidWhen(Func<TValidatableEntity, bool> invalidCondition)
        {
            _invalidConditions.Add(invalidCondition);
            return this;
        }

        /// <inheritdoc/>
        public IRuleBuilder<TValidatableEntity> ExceptWhen(Func<TValidatableEntity, bool> suppressCondition)
        {
            _suppressConditions.Add(suppressCondition);
            return this;
        }

        /// <summary>
        /// Static method to create a new rule. This hides the constructor from external assemblies.
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="uiHandle"></param>
        /// <returns></returns>
        internal static Rule<TValidatableEntity> Create(string errorMessage, string uiHandle)
        {
            return new Rule<TValidatableEntity>(errorMessage, uiHandle);
        }
    }
}
