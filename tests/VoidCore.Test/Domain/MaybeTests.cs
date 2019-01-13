using System;
using VoidCore.Domain;
using Xunit;

namespace VoidCore.Test.Domain
{
    public class MaybeTests
    {
        [Fact]
        public void AccessingMaybeValueWithoutValueThrowsInvalidOperationException()
        {
            var maybe = Maybe<string>.None;
            Assert.Throws<InvalidOperationException>(() =>
            {
                var unused = maybe.Value;
            });
        }

        [Fact]
        public void MaybeFromClassHasValue()
        {
            var maybe = Maybe.From("some value");
            Assert.False(maybe.HasNoValue);
            Assert.True(maybe.HasValue);
            Assert.Equal("some value", maybe.Value);
        }

        [Fact]
        public void MaybeFromUsingNonGenericHelper()
        {
            var maybe1 = Maybe.From("some value");
            var maybe2 = Maybe.From("some value");
            Assert.False(maybe1.HasNoValue);
            Assert.True(maybe1.HasValue);
            Assert.Equal("some value", maybe1.Value);
            Assert.Equal(maybe1, maybe2);
        }

        [Fact]
        public void MaybeFromIntHasValue()
        {
            var maybe = Maybe.From(2);
            Assert.False(maybe.HasNoValue);
            Assert.True(maybe.HasValue);
            Assert.Equal(2, maybe.Value);
        }

        [Fact]
        public void MaybeFromIntNoneHasNoValue()
        {
            var maybe = Maybe<int>.None;
            Assert.True(maybe.HasNoValue);
            Assert.False(maybe.HasValue);
        }

        [Fact]
        public void MaybeFromNullHasNoValue()
        {
            var maybe = Maybe.From<string>(null);
            Assert.True(maybe.HasNoValue);
            Assert.False(maybe.HasValue);
        }

        [Fact]
        public void MaybeGetHashCodeReturnsValueHashCodeExceptWhenNone()
        {
            var value = "some value";
            var maybe = Maybe.From(value);
            Assert.Equal(value.GetHashCode(), maybe.GetHashCode());

            maybe = Maybe<string>.None;
            Assert.Equal(0, maybe.GetHashCode());
        }

        [Fact]
        public void MaybeHasImplicitConversionFromValueType()
        {
            var maybe = Maybe.From("some value");
            Assert.False(maybe.HasNoValue);
            Assert.True(maybe.HasValue);
            Assert.Equal("some value", maybe.Value);
        }

        [Fact]
        public void MaybeIsEqualToMaybeAsObject()
        {
            var maybe1 = Maybe.From("some value");
            var maybe2 = Maybe.From("some value") as object;
            Assert.Equal(maybe1, maybe2);
        }

        [Fact]
        public void MaybeIsEqualToValueAsObject()
        {
            var maybe = Maybe.From("some value");
            object other = "some value";
            Assert.Equal(maybe, other);
        }

        [Fact]
        public void MaybeIsEqualWithItsValue()
        {
            var maybe = Maybe.From("some value");
            Assert.True(maybe == "some value");
            Assert.False(maybe != "some value");
        }

        [Fact]
        public void MaybeIsEqualWithSameValuedMaybe()
        {
            var maybe1 = Maybe.From("some value");
            var maybe2 = Maybe.From("some value");
            Assert.True(maybe1 == maybe2);
            Assert.False(maybe1 != maybe2);
        }

        [Fact]
        public void MaybeIsNotEqualToAnotherObject()
        {
            var maybe = Maybe.From("some value");
            object other = 3;
            Assert.NotEqual(maybe, other);
        }

        [Fact]
        public void MaybeIsNotEqualWithDifferentValuedMaybe()
        {
            var maybe1 = Maybe.From("some value");
            var maybe2 = Maybe.From("some other value");
            Assert.True(maybe1 != maybe2);
            Assert.False(maybe1 == maybe2);
        }

        [Fact]
        public void MaybeIsNotEqualWithNotItsValue()
        {
            var maybe = Maybe.From("some value");
            Assert.True(maybe != "some other value");
            Assert.False(maybe == "some other value");
        }

        [Fact]
        public void MaybeNoneHasNoValue()
        {
            var maybe = Maybe<string>.None;
            Assert.True(maybe.HasNoValue);
            Assert.False(maybe.HasValue);
        }

        [Fact]
        public void MaybeToString()
        {
            var maybe = Maybe.From("some value");
            Assert.Equal("some value", maybe.ToString());

            maybe = Maybe<string>.None;
            Assert.Equal("No value", maybe.ToString());
        }

        [Fact]
        public void NoneMaybeHasInequalityWithMaybe()
        {
            var maybe1 = Maybe.From("some value");
            var maybe2 = Maybe.From("some other value");
            Assert.True(maybe1 != maybe2);
            Assert.False(maybe1 == maybe2);
        }

        [Fact]
        public void NoneMaybeHasInequalityWithValue()
        {
            var maybe = Maybe.From("some value");
            Assert.True(maybe != "some other value");
            Assert.False(maybe == "some other value");
        }

        [Fact]
        public void NoneMaybeIsEqualToAnotherNoneMaybe()
        {
            var maybe1 = Maybe<string>.None;
            var maybe2 = Maybe<string>.None;
            Assert.True(maybe1 == maybe2);
            Assert.False(maybe1 != maybe2);
        }

        [Fact]
        public void NoneMaybeIsNotEqualToValueOrMaybeOfValue()
        {
            var maybe = Maybe<string>.None;
            Assert.False(maybe == "some value");
            Assert.False(maybe == null);
            Assert.True(maybe != "some value");
            Assert.True(maybe != null);

            var maybe2 = Maybe.From("some value");
            Assert.False(maybe == maybe2);
            Assert.True(maybe != maybe2);
            Assert.False(maybe2 == maybe);
            Assert.True(maybe2 != maybe);
        }
    }
}
