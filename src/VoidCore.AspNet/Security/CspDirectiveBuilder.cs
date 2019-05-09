using System.Collections.Generic;

namespace VoidCore.AspNet.Security
{
    /// <summary>
    /// Builds CSP header directives. Adapted from https://www.c-sharpcorner.com/article/using-csp-header-in-asp-net-core-2-0/
    /// </summary>
    public sealed class CspDirectiveBuilder
    {
        private readonly List<string> _sources = new List<string>();

        internal CspDirectiveBuilder(string name)
        {
            Name = name;
        }

        internal IReadOnlyList<string> Sources => _sources;
        internal string Name { get; }

        /// <summary>
        /// Uses a wildcard to allow any source.
        /// </summary>
        public CspDirectiveBuilder AllowAny() => Allow("*");

        /// <summary>
        /// Allow no sources.
        /// </summary>
        public CspDirectiveBuilder AllowNone() => Allow("'none'");

        /// <summary>
        /// Allow sources from the origin site.
        /// </summary>
        public CspDirectiveBuilder AllowSelf() => Allow("'self'");

        /// <summary>
        /// Allow inline sources defined between tags.
        /// </summary>
        public CspDirectiveBuilder AllowUnsafeInline() => Allow("'unsafe-inline'");

        /// <summary>
        /// Allow the use of eval() to create code from strings.
        /// </summary>
        public CspDirectiveBuilder AllowUnsafeEval() => Allow("'unsafe-eval'");

        /// <summary>
        /// Use a nonce generated by the server upon every request to whitelist sources.
        /// </summary>
        /// <param name="base64Value">The base 64 nonce value used to identify the source.</param>
        public CspDirectiveBuilder AllowNonce(string base64Value) => Allow($"'nonce-{base64Value}'");

        /// <summary>
        /// Use a sha256, sha384, sha512 hash to identify scripts or styles.
        /// </summary>
        /// <param name="algorithm">The string name of the algorithm used to derive the hash.</param>
        /// <param name="base64Value">The base 64 value of the hash.</param>
        public CspDirectiveBuilder AllowHash(string algorithm, string base64Value) => Allow($"'{algorithm}-{base64Value}'");

        /// <summary>
        /// Allow a source by URI.
        /// </summary>
        /// <param name="source">The source URI</param>
        public CspDirectiveBuilder Allow(string source)
        {
            _sources.Add(source);
            return this;
        }

        /// <summary>
        /// Build the directive string value.
        /// </summary>
        /// <returns>The string representation of the header directive.</returns>
        public string Build()
        {
            return $"{Name} {string.Join(" ", _sources)}; ";
        }
    }
}