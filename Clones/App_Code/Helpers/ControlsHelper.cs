using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Text.RegularExpressions;

namespace Clones.Helpers
{
    public static class ControlsHelper
    {
        public static MvcHtmlString EditorNew(this HtmlHelper html, string expression, object value, string fieldName, Dictionary<string, object> htmlAttributes = null)
        {
            var res = EditorExtensions.Editor(html, expression, "", fieldName, new { htmlAttributes });

            string pattern = "value=\"0*\"";
            Regex regex = new Regex(pattern);

            var resString = res.ToString();

            var newRes = regex.Replace(resString, $"value=\"{value}\"");

            return new MvcHtmlString(newRes);
        }

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

        public static ModelScopeContext<TNewModel> ModelScope<TNewModel>(this HtmlHelper helper, TNewModel model, string htmlPrefix = null)
        {
            var newHelper = For<TNewModel>(helper, model, htmlPrefix);
            return new ModelScopeContext<TNewModel>(newHelper);
        }

        public static ModelScopeContext<TNewModel> ModelScopeFor<TModel, TNewModel>(this HtmlHelper<TModel> helper, System.Linq.Expressions.Expression<Func<TModel, TNewModel>> expression, string htmlPrefix = null)
        {
            return ModelScope(helper, expression.Compile()(helper.ViewData.Model));
        }

        public static HtmlHelper<TModel> For<TModel>(this HtmlHelper helper, string htmlPrefix = null) where TModel : class, new()
        {
            return For<TModel>(helper.ViewContext, helper.ViewDataContainer.ViewData, helper.RouteCollection);
        }

        public static HtmlHelper<TModel> For<TModel>(this HtmlHelper helper, TModel model, string htmlPrefix = null)
        {
            return For<TModel>(helper.ViewContext, helper.ViewDataContainer.ViewData, helper.RouteCollection, model);
        }

        public static HtmlHelper<TModel> For<TModel>(this ViewContext viewContext, ViewDataDictionary viewData, RouteCollection routeCollection, string htmlPrefix = null) where TModel : class, new()
        {
            TModel model = new TModel();
            return For<TModel>(viewContext, viewData, routeCollection, model);
        }

        public static HtmlHelper<TModel> For<TModel>(this ViewContext viewContext, ViewDataDictionary viewData, RouteCollection routeCollection, TModel model, string htmlPrefix = null)
        {
            var newViewData = new ViewDataDictionary(viewData) { Model = model };
            ViewContext newViewContext = new ViewContext(viewContext.Controller.ControllerContext, viewContext.View, newViewData, viewContext.TempData, viewContext.Writer);

            newViewData.TemplateInfo.HtmlFieldPrefix = htmlPrefix;
            var viewDataContainer = new ViewDataContainer(newViewContext.ViewData);

            return new HtmlHelper<TModel>(newViewContext, viewDataContainer, routeCollection);
        }

        private class ViewDataContainer : IViewDataContainer
        {
            public ViewDataContainer(ViewDataDictionary viewData)
            {
                ViewData = viewData;
            }

            public ViewDataDictionary ViewData { get; set; }
        }

        public class ModelScopeContext<TModel> : IDisposable
        {
            public ModelScopeContext(HtmlHelper<TModel> html)
            {
                Html = html;
                Model = Html.ViewData.Model;
            }

            public HtmlHelper<TModel> Html { get; protected set; }
            public TModel Model { get; protected set; }

            #region IDisposable Support
            private bool disposedValue = false; // To detect redundant calls

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        // TODO: dispose managed state (managed objects).
                    }

                    // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                    // TODO: set large fields to null.

                    disposedValue = true;
                }
            }

            // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
            // ~ModelScopeContext() {
            //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            //   Dispose(false);
            // }

            // This code added to correctly implement the disposable pattern.
            public void Dispose()
            {
                // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
                Dispose(true);
                // TODO: uncomment the following line if the finalizer is overridden above.
                GC.SuppressFinalize(this);
            }
            #endregion

        }
    }
}