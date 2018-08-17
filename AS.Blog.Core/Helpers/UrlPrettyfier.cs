using AS.Extensions;
using System.Net;
using System.Text;

namespace AS.Blog.Core.Helpers
{
    public static class UrlPrettyfier
    {
        public static string Pretty(this string s)
        {
            var tmp = Encoding.Convert(Encoding.Default, Encoding.ASCII, s.ToBytes()).AsString();
            tmp = tmp.Replace(' ', '-');
            return WebUtility.UrlEncode(tmp);
        }
    }
}
