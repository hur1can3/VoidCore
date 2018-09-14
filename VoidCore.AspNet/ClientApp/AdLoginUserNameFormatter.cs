using System.Linq;
using VoidCore.Model.ClientApp;

namespace VoidCore.AspNet.ClientApp
{
    /// <summary>
    /// Get the samAccountName from a fully-qualified AD login.
    /// </summary>
    public class AdLoginUserNameFormatter : IUserNameFormatter
    {
        /// <summary>
        /// Get the user name from a fully-qualified AD login.
        /// Eg: DOMAIN1\UserName returns UserName
        /// </summary>
        /// <param name="fullUserName"></param>
        /// <returns></returns>
        public string Format(string fullUserName)
        {
            return fullUserName?.Split("\\").LastOrDefault() ?? "Unknown";
        }
    }
}
