using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Clones.Helpers
{
    public static class ControlsHelper
    {
        public static MvcHtmlString EditorForNew<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string editorName, object additionalViewData = null)
        {
            return EditorExtensions.EditorFor(html, expression, "name", editorName, additionalViewData);
        }

        public static MvcHtmlString ValidationMessageForNew<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string editorName, IDictionary<string, object> htmlAttributes = null)
        {
            if (htmlAttributes == null)
                htmlAttributes = new Dictionary<string, object>();

            if (!htmlAttributes.ContainsKey("data-valmsg-for"))
                htmlAttributes.Add("data-valmsg-for", editorName);

            return ValidationExtensions.ValidationMessageFor(htmlHelper, expression, "", htmlAttributes);
        }

        public static MvcHtmlString HiddenForNew<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string editorName, IDictionary<string, object> htmlAttributes = null)
        {
            if (htmlAttributes == null)
                htmlAttributes = new Dictionary<string, object>();

            if (!htmlAttributes.ContainsKey("id"))
                htmlAttributes.Add("id", editorName);

            return InputExtensions.HiddenFor(htmlHelper, expression, htmlAttributes);
        }
    }
}