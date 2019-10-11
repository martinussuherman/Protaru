using System;
using Microsoft.AspNetCore.Http;

namespace MonevAtr
{
    public static class PagerUrlHelper
    {
        public static int ItemPerPage { get; set; }

        public static string RetrievePagedUrl(HttpContext context, int page)
        {
            // case:
            //          http://localhost/test/url
            //          http://localhost/test/url?page=1
            //          http://localhost/test/url?param1=a&param2=b
            //          http://localhost/test/url?param1=a&param2=b&page=1

            string rawTarget = context.Request.GetRawTarget();

            if (!rawTarget.Contains('?'))
            {
                return rawTarget + "?page=" + page;
            }

            if (rawTarget.Contains("?page="))
            {
                return rawTarget.Split("?page=")[0] + "?page=" + page;
            }

            return rawTarget.Split("&page=")[0] + "&page=" + page;
        }
    }
}