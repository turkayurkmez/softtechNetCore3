using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Filters.Filters
{
    public class NotImplementedAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is NotImplementedException)
            {
                string message = $"{context.Exception.Message} : {context.Exception.StackTrace} Source: {context.Exception.Source}";

                context.Result = new ViewResult()
                {
                    ViewName = "NotImplemented",
                    ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                    {
                        Model = message
                    }

                };




            }
        }
    }
}
