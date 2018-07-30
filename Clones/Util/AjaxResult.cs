using System.Web.Mvc;

namespace Clones.Util
{
    public class AjaxResult : JsonResult
    {
        public AjaxResult(AjaxResultState resultState, object value = null, JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.DenyGet)
        {
            Data = new
            {
                ResultState = resultState,
                Value = value
            };
            JsonRequestBehavior = jsonRequestBehavior;
        }
    }

    public enum AjaxResultState
    {
        OK = 1,
        Error = -1,
        Redirect = 2
    }
}