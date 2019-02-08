using Moq;
using VoidCore.Domain;
using Xunit;

namespace VoidCore.Test.Domain
{
    public class FunctionalExtensionsTests
    {
        [Fact]
        public void MapIntegerReturnsMappedValue()
        {
            var myInt = 2;

            var actual = myInt.Map(i => i + 100);

            Assert.Equal(102, actual);
        }

        [Fact]
        public void MapStringArrayReturnsMappedValue()
        {
            var myStrings = new [] { "H", "ello", " World" };

            var actual = myStrings.Map(i => string.Join("", i));

            Assert.Equal("Hello World", actual);
        }

        [Fact]
        public void TeeIntegerReturnsSameValueAsInput()
        {
            var myInt = 2;

            var actual = myInt.Tee(i => i = i + 100);

            Assert.Equal(2, actual);
        }

        [Fact]
        public void TeeStringArrayReturnsSameReferenceAsInput()
        {
            var myStrings = new [] { "H", "ello", " World" };

            var actual = myStrings.Tee(strs => strs[0] = "O");

            Assert.Same(myStrings, actual);
        }

        [Fact]
        public void TeeStringActionWithoutType()
        {
            var myStrings = new [] { "H", "ello", " World" };

            var actionableMock = new Mock<IActionable>();
            actionableMock.Setup(a => a.Go());

            var actual = myStrings.Tee(actionableMock.Object.Go);

            actionableMock.Verify(a => a.Go(), Times.Once);

            Assert.Same(myStrings, actual);
        }

        public interface IActionable
        {
            void Go();
        }
    }
}
