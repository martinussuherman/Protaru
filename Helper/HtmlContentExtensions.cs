using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;

namespace MonevAtr
{
    public static class HtmlContentExtensions
    {
        public static string ContentAsString(this IHtmlContent content)
        {
            using StringWriter writer = new StringWriter();
            content.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }
    }
}