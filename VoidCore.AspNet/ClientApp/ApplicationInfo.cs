using System.Collections.Generic;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using VoidCore.Model.ClientApp;

namespace VoidCore.AspNet.ClientApp
{
    /// <inheritdoc/>
    public class ApplicationInfo : IApplicationInfo
    {
        /// <inheritdoc/>
        public string ApplicationName { get; }

        /// <inheritdoc/>
        public string AntiforgeryToken { get; }

        /// <inheritdoc/>
        public string AntiforgeryTokenHeaderName { get; }

        /// <inheritdoc/>
        public string UserName { get; }

        /// <inheridoc/>
        public IEnumerable<string> UserPolicies { get; }

        /// <summary>
        /// Construct a new ApplicaitonInfo object.
        /// </summary>
        /// <param name="applicationSettings">General application settings</param>
        /// <param name="httpContextAccessor">The HttpContext</param>
        /// <param name="antiforgery">The ASPNET antiforgery object</param>
        /// <param name="currentUser">UI-friendly user name</param>
        public ApplicationInfo(ApplicationSettings applicationSettings, IHttpContextAccessor httpContextAccessor, IAntiforgery antiforgery, ICurrentUser currentUser)
        {
            ApplicationName = applicationSettings.Name ?? "Application";
            AntiforgeryToken = antiforgery.GetAndStoreTokens(httpContextAccessor.HttpContext).RequestToken;
            AntiforgeryTokenHeaderName = antiforgery.GetAndStoreTokens(httpContextAccessor.HttpContext).HeaderName;
            UserName = currentUser.Name;
            UserPolicies = currentUser.Policies;
        }
    }
}
