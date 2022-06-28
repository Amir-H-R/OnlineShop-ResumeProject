using System.Security.Claims;

namespace Endpoint.Site.Utilities
{
    public class ClaimUtilities
    {
        public static long? GetUserId(ClaimsPrincipal user)
        {
            try
            {

                var claimsIdentity = user.Identity as ClaimsIdentity; 
                if (claimsIdentity.FindFirst(ClaimTypes.NameIdentifier) != null)
                {
                    long userId = long.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
                    return userId;
                }
                else 
                {
                    return null;
                }

            }
            catch
            {
                return null;
            }
        }
    }
}
