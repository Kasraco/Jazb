using System;
using System.Web;
using System.Web.Caching;

namespace Jazb.Web
{
    public static class CacheProfileName
    {
        public const string BadRequest = "BadRequest";
        public const string BrowserConfigXml = "BrowserConfigXml";
        public const string Feed = "Feed";
        public const string Forbidden = "Forbidden";
        public const string InternalServerError = "InternalServerError";
        public const string ManifestJson = "ManifestJson";
        public const string MethodNotAllowed = "MethodNotAllowed";
        public const string NotFound = "NotFound";
        public const string OpenSearchXml = "OpenSearchXml";
        public const string RobotsText = "RobotsText";
        public const string Unauthorized = "Unauthorized";
    }

}
namespace Jazb.Web.Infrastructure
{
   
    public static class CacheManager
    {
        public static void CacheInsert(this HttpContextBase httpContext, string key, object data, int durationMinutes)
        {
            if (data == null) return;
            httpContext.Cache.Add(
                key,
                data,
                null,
                DateTime.Now.AddMinutes(durationMinutes),
                TimeSpan.Zero,
                CacheItemPriority.AboveNormal,
                null);
        }

        public static T CacheRead<T>(this HttpContextBase httpContext, string key)
        {
            object data = httpContext.Cache[key];
            if (data != null)
                return (T)data;
            return default(T);
        }

        public static void InvalidateCache(this HttpContextBase httpContext, string key)
        {
            httpContext.Cache.Remove(key);
        }
    }
}