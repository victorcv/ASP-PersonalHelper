using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PersonalHelper.Extensions.Helpers
{
    public static class Helpers { 

    public static MvcHtmlString ImageActionLink(
                 this HtmlHelper helper, string action, string imagePath, string altText, string hoverImage = null)
    {
        return ImageActionLink(helper, action, null, null, imagePath, altText, hoverImage);
    }

    public static MvcHtmlString ImageActionLink(
             this HtmlHelper helper, string action, string controller,
             object parameters, string imagePath, string altText, string hoverImage = null)
    {

        TagBuilder image = new TagBuilder("img");

        string imgPath = UrlHelper.GenerateContentUrl(imagePath,
                                              helper.ViewContext.HttpContext);
        image.MergeAttribute("src", imgPath);
        image.MergeAttribute("alt", altText);
        if (!string.IsNullOrEmpty(hoverImage))
        {
            string hoverImgPath = UrlHelper.GenerateContentUrl(hoverImage,
                                                  helper.ViewContext.HttpContext);
            image.MergeAttribute("data-img", imgPath);
            image.MergeAttribute("data-hover", hoverImgPath);
        }

        string url = UrlHelper.GenerateUrl(null, action, controller,
                                 new RouteValueDictionary(parameters),
                                 helper.RouteCollection,
                                 helper.ViewContext.RequestContext, true);

        TagBuilder link = new TagBuilder("a");
        link.MergeAttribute("href", url);

        link.InnerHtml = image.ToString(TagRenderMode.SelfClosing);

        return MvcHtmlString.Create(link.ToString());
    }
}
}