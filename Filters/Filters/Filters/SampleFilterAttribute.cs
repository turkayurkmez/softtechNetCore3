using Microsoft.AspNetCore.Mvc;

namespace Filters.Filters
{
    public class SampleFilterAttribute : TypeFilterAttribute
    {
        public SampleFilterAttribute() : base(typeof(SampleFilter))
        {
        }
    }
}
