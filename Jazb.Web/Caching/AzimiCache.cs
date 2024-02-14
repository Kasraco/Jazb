using System;
using System.Configuration;
using System.Web;
using Jazb.Model;
using Jazb.Servicelayer.Interfaces;
using Jazb.Web.Infrastructure;

namespace Jazb.Web.Caching
{
    public class JazbCache
    {
        public const string SiteConfigKey = "SiteConfig";

        public static SiteConfig GetSiteConfig(HttpContextBase httpContext, IOptionService optionService)
        {
            var siteConfig = httpContext.CacheRead<SiteConfig>(SiteConfigKey);
            int durationMinutes =
                Convert.ToInt32(ConfigurationManager.AppSettings["CacheOptionsDuration"]);

            if (siteConfig == null)
            {
                siteConfig = optionService.GetAll();
                httpContext.CacheInsert(SiteConfigKey, siteConfig, durationMinutes);
            }
            return siteConfig;
        }

        public static void RemoveSiteConfig(HttpContextBase httpContext)
        {
            httpContext.InvalidateCache(SiteConfigKey);
        }



        public static object GetChacheSite(HttpContextBase httpContext,string Key, Object ObjectServiceService)
        {
            var siteConfig = httpContext.CacheRead<object>(Key);
            int durationMinutes =
                Convert.ToInt32(ConfigurationManager.AppSettings["CacheOptionsDuration"]);

            if (siteConfig == null)
            {
                httpContext.CacheInsert(Key, ObjectServiceService, durationMinutes);
            }
            return siteConfig;
        }

        public static void RemoveChacheSite(HttpContextBase httpContext,string Key)
        {
            httpContext.InvalidateCache(Key);
        }
    }
}