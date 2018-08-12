using Microsoft.AspNetCore.Identity;

namespace AS.Blog.Core.Store
{
    public class CustomLookupNormalizer : ILookupNormalizer
    {
        public string Normalize(string key)
        {
            return key.ToLower();
        }
    }
}
