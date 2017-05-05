using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Spending.Util
{
    public static class ImageActionLinkHelper
    {
        public static string ImageActionLink(this AjaxHelper helper, string imageUrl, string altText, string actionName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", imageUrl);
            builder.MergeAttribute("alt", altText);
            var link = helper.ActionLink("[replaceme]", actionName, routeValues, ajaxOptions, htmlAttributes);
            return link.ToHtmlString().Replace("[replaceme]", builder.ToString(TagRenderMode.SelfClosing));
        }
    }
}