using VoidCore.AspNet.Attributes;
using Xunit;

namespace VoidCore.Test.AspNet.Attributes
{
    public class AutoPolicyAuthorizeAttributeTests
    {
        [Fact]
        public void PolicyIsSetBasedOnAttributeName()
        {
            var att = new TesterOnly();
            Assert.Equal("Tester", att.Policy);
        }
    }

    public class TesterOnly : AutoPolicyAuthorizeAttribute { }
}