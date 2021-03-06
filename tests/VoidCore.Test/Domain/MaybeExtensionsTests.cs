using System.Linq;
using System.Threading.Tasks;
using VoidCore.Domain;
using Xunit;

namespace VoidCore.Test.Domain
{
    public class MaybeExtensionsTests
    {
        [Fact]
        public void MaybeToResultWithoutValueIsFailed()
        {
            var maybe = Maybe<string>.None;
            var result = maybe.ToResult(new Failure("no value", "uiField"));

            Assert.True(result.IsFailed);
            var failure = result.Failures.Single();
            Assert.Equal("no value", failure.Message);
            Assert.Equal("uiField", failure.UiHandle);
        }

        [Fact]
        public void MaybeToResultWithValueIsSuccessful()
        {
            var result = Maybe.From("some value").ToResult(new Failure("no value"));

            Assert.True(result.IsSuccess);
            Assert.Equal("some value", result.Value);
        }

        [Fact]
        public async Task MaybeToResultAsyncWithoutValueIsFailed()
        {
            var maybeTask = Task.FromResult(Maybe<string>.None);

            var result = await maybeTask.ToResultAsync(new Failure("no value", "uiField"));

            Assert.True(result.IsFailed);
            var failure = result.Failures.Single();
            Assert.Equal("no value", failure.Message);
            Assert.Equal("uiField", failure.UiHandle);
        }

        [Fact]
        public async Task MaybeToResultAsyncWithValueIsSuccessful()
        {
            var maybeTask = Task.FromResult(Maybe.From("some value"));

            var result = await maybeTask.ToResultAsync(new Failure("no value", "uiField"));

            Assert.True(result.IsSuccess);
            Assert.Equal("some value", result.Value);
        }

        [Fact]
        public void MaybeSelectWithoutValueReturnsNone()
        {
            var maybe = Maybe<string>.None
                .Select(v => 2);

            Assert.IsType<Maybe<int>>(maybe);
            Assert.True(maybe.HasNoValue);
        }

        [Fact]
        public async Task MaybeSelectAsyncWithoutValueReturnsNone()
        {
            var t = new TestTransformerService();

            var maybe = await Maybe<string>.None
                .SelectAsync(a => t.TransformAsync(a, 1))
                .SelectAsync(a => t.Transform(a, 2))
                .SelectAsync(a => t.TransformAsync(a, 3));

            Assert.True(maybe.HasNoValue);
        }

        [Fact]
        public void MaybeSelectWithValueReturnsTransformation()
        {
            var maybe = Maybe.From("some value")
                .Select(v => 2);

            Assert.IsType<Maybe<int>>(maybe);
            Assert.Equal(2, maybe.Value);
        }

        [Fact]
        public async Task MaybeSelectAsyncWithValueReturnsTransformation()
        {
            var t = new TestTransformerService();

            var maybe = await Maybe.From(TestTransformerService.Start)
                .SelectAsync(a => t.TransformAsync(a, 1))
                .SelectAsync(a => t.Transform(a, 2))
                .SelectAsync(a => t.TransformAsync(a, 3));

            Assert.True(maybe.HasValue);
            Assert.Equal("Hello World!!!", maybe.Value);
        }

        [Fact]
        public void MaybeThenWithoutValueReturnsNone()
        {
            var maybe = Maybe<string>.None
                .Then(v => Maybe.From(2));

            Assert.IsType<Maybe<int>>(maybe);
            Assert.True(maybe.HasNoValue);
        }

        [Fact]
        public async Task MaybeThenAsyncWithoutValueReturnsNone()
        {
            var t = new TestTransformerService();

            var maybe = await Maybe<string>.None
                .ThenAsync(a => t.TransformMaybeAsync(a, 1))
                .ThenAsync(a => t.TransformMaybe(a, 2))
                .ThenAsync(a => t.TransformMaybeAsync(a, 3));

            Assert.True(maybe.HasNoValue);
        }

        [Fact]
        public void MaybeThenWithValueReturnsTransformation()
        {
            var maybe = Maybe.From("some value")
                .Then(v => Maybe.From(2));

            Assert.IsType<Maybe<int>>(maybe);
            Assert.Equal(2, maybe.Value);
        }

        [Fact]
        public async Task MaybeThenAsyncWithValueReturnsTransformation()
        {
            var t = new TestTransformerService();

            var maybe = await Maybe.From(TestTransformerService.Start)
                .ThenAsync(a => t.TransformMaybeAsync(a, 1))
                .ThenAsync(a => t.TransformMaybe(a, 2))
                .ThenAsync(a => t.TransformMaybeAsync(a, 3));

            Assert.True(maybe.HasValue);
            Assert.Equal("Hello World!!!", maybe.Value);
        }

        [Fact]
        public void MaybeUnwrapWithoutValue()
        {
            var maybe = Maybe<string>.None;

            var valueNoDefault = maybe.Unwrap();
            Assert.Null(valueNoDefault);

            var valueWithDefault = maybe.Unwrap("default value");
            Assert.Equal("default value", valueWithDefault);

            var valueWithDefaultFactory = maybe.Unwrap(() => "default value");
            Assert.Equal("default value", valueWithDefaultFactory);
        }

        [Fact]
        public async Task MaybeUnwrapAsyncWithoutValue()
        {
            var maybe = Task.FromResult(Maybe<string>.None);

            var valueNoDefault = await maybe.UnwrapAsync();
            Assert.Null(valueNoDefault);

            var valueWithDefault = await maybe.UnwrapAsync("default value");
            Assert.Equal("default value", valueWithDefault);

            var valueWithDefaultFactory = await maybe.UnwrapAsync(() => "default value");
            Assert.Equal("default value", valueWithDefaultFactory);
        }

        [Fact]
        public void MaybeUnwrapWithValue()
        {
            Maybe<string> maybe = "some value";

            var valueNoDefault = maybe.Unwrap();
            Assert.Equal("some value", valueNoDefault);

            var valueWithDefault = maybe.Unwrap("default value");
            Assert.Equal("some value", valueWithDefault);

            var valueWithDefaultFactory = maybe.Unwrap(() => "default value");
            Assert.Equal("some value", valueWithDefaultFactory);
        }

        [Fact]
        public async Task MaybeUnwrapAsyncWithValue()
        {
            var maybe = Task.FromResult(Maybe.From("some value"));

            var valueNoDefault = await maybe.UnwrapAsync();
            Assert.Equal("some value", valueNoDefault);

            var valueWithDefault = await maybe.UnwrapAsync("default value");
            Assert.Equal("some value", valueWithDefault);

            var valueWithDefaultFactory = await maybe.UnwrapAsync(() => "default value");
            Assert.Equal("some value", valueWithDefaultFactory);
        }

        [Fact]
        public void WhereWithValueAndFalsePredicateReturnsNone()
        {
            Maybe<string> maybe = "some value";
            var queried = maybe.Where(v => false);

            Assert.True(queried.HasNoValue);
        }

        [Fact]
        public void WhereWithoutValueAndTruePredicateReturnsNone()
        {
            var maybe = Maybe<string>.None;
            var queried = maybe.Where(v => true);

            Assert.True(queried.HasNoValue);
        }

        [Fact]
        public void WhereWithValueAndTruePredicateReturnsMaybe()
        {
            Maybe<string> maybe = "some value";
            var queried = maybe.Where(v => true);

            Assert.True(queried.HasValue);
            Assert.Equal("some value", queried.Value);
        }

        [Fact]
        public async Task WhereAsyncWithValueAndFalsePredicateReturnsNone()
        {
            var maybe = Task.FromResult(Maybe.From("some value"));
            var queried = await maybe.WhereAsync(v => false);

            Assert.True(queried.HasNoValue);
        }

        [Fact]
        public async Task WhereAsyncWithoutValueAndTruePredicateReturnsNone()
        {
            var maybe = Task.FromResult(Maybe<string>.None);
            var queried = await maybe.WhereAsync(v => true);

            Assert.True(queried.HasNoValue);
        }

        [Fact]
        public async Task WhereAsyncWithValueAndTruePredicateReturnsMaybe()
        {
            var maybe = Task.FromResult(Maybe.From("some value"));
            var queried = await maybe.WhereAsync(v => true);

            Assert.True(queried.HasValue);
            Assert.Equal("some value", queried.Value);
        }
    }
}
