using Filters.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Filters.Filters
{
    public class StopwatchAttribute : ActionFilterAttribute
    {
        private Stopwatch _stopWatch;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _stopWatch = Stopwatch.StartNew();

        }

        public override async void OnActionExecuted(ActionExecutedContext context)
        {
            _stopWatch.Stop();

            /*
             * 1. Model
             * 2. View
             * 3. Response.Write()
             */
            var viewResult = context.Result as ViewResult;
            var model = (ModelBase)viewResult.Model;
            if (model != null)
            {
                model.Seconds = _stopWatch.Elapsed.TotalSeconds;
            }
            else
            {
                //if (viewResult.StatusCode == 200)
                //{
                //await context.HttpContext.Response.WriteAsync($"Calisan sure:{_stopWatch.Elapsed.TotalSeconds}");
                viewResult.ViewData["Stopwatch"] = _stopWatch.Elapsed.TotalSeconds;
                //}
                //else
                //{
                //  await context.HttpContext.Response.WriteAsync($"Calisan sure:{_stopWatch.Elapsed.TotalSeconds}");
                //}
            }

            _stopWatch.Reset();
            if (_stopWatch.IsRunning)
            {
                _stopWatch.Stop();
            }
        }
    }
}
