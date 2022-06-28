

namespace Endpoint.Site.Utilities
{
    public interface ICookieManager
    {
         void Add(HttpContext context, string token, string value);
         bool Contains(HttpContext context, string toekn);
         string GetValue(HttpContext context, string token);
         Guid GetBrowserId(HttpContext context);
         void Remove(HttpContext context, string toekn);
    }

    public class CookieManager : ICookieManager
    {
        public void Add(HttpContext context, string token, string value)
        {
            context.Response.Cookies.Append(token, value, getCookieOptions(context));
        }

        public bool Contains(HttpContext context, string toekn)
        {
            return context.Request.Cookies.ContainsKey(toekn);
        }

        public string GetValue(HttpContext context, string token)
        {
            string cookieValue;
            if (!context.Request.Cookies.TryGetValue(token, out cookieValue))
            {
                return null;
            }
            return cookieValue;
        }

        public Guid GetBrowserId(HttpContext context)
        {

            string browserId = GetValue(context, "obId");
            if (browserId == null)
            {
                string value = Guid.NewGuid().ToString();
                Add(context, "obId", value);
                browserId = value;
            }
            Guid browserGuid;
            Guid.TryParse(browserId, out browserGuid);
            return browserGuid;
        }

        public void Remove(HttpContext context, string toekn)
        {
            if (context.Request.Cookies.ContainsKey(toekn))
            {
                context.Response.Cookies.Delete(toekn);
            }
        }

        private CookieOptions getCookieOptions(HttpContext context)
        {
            return new CookieOptions
            {
                HttpOnly = true,
                Path = context.Request.PathBase.HasValue ? context.Request.PathBase.ToString() : "/",
                Secure = context.Request.IsHttps,
                Expires = DateTime.Now.AddDays(100),
            };
        }
    }
}
