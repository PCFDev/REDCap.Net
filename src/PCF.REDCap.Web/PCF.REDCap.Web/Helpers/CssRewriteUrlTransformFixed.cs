using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Optimization;

namespace PCF.REDCap.Web.Helpers
{
    /// <summary>
    /// Fix for the standard System.Web.Optimization.CssRewriteUrlTransform which doesn't play nice with data URIs.
    /// I've logged the bug on codeplex, but this will have to do for now: https://aspnetoptimization.codeplex.com/workitem/88
    /// </summary>
    public class CssRewriteUrlTransformFixed : IItemTransform
    {
        //Do we care about quote matching?
        private static Regex UrlRegex = new Regex(@"url\(['""]?(?<url>[^)]+?)['""]?\)", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);

        public string Process(string includedVirtualPath, string input)
        {
            if (includedVirtualPath == null)
            {
                throw new ArgumentNullException("includedVirtualPath");
            }

            var directory = VirtualPathUtility.GetDirectory(includedVirtualPath.Substring(1));
            return ConvertUrlsToAbsolute(directory, input);
        }

        private static string ConvertUrlsToAbsolute(string baseUrl, string content)
        {
            if (String.IsNullOrWhiteSpace(content))
            {
                return content;
            }
            return UrlRegex.Replace(content, (Match match) => "url(" + RebaseUrlToAbsolute(baseUrl, match.Groups["url"].Value) + ")");
        }

        private static string RebaseUrlToAbsolute(string baseUrl, string url)
        {
            if (String.IsNullOrWhiteSpace(url) ||
                String.IsNullOrWhiteSpace(baseUrl) ||
                url.StartsWith("/", StringComparison.Ordinal) ||
                url.StartsWith("data:", StringComparison.Ordinal) ||
                url.StartsWith("http://", StringComparison.Ordinal) ||
                url.StartsWith("https://", StringComparison.Ordinal))
            {
                return url;
            }

            if (!baseUrl.EndsWith("/", StringComparison.Ordinal))
            {
                baseUrl += "/";
            }

            return VirtualPathUtility.ToAbsolute(baseUrl + url);
        }
    }
}
