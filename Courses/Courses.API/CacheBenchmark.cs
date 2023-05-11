using Courses.DataTransferObjects.Responses;

namespace Courses.API
{
    public class CacheBenchmark
    {
        public DateTime CacheTime { get; set; }
        public IEnumerable<CourseSummaryResponse> Courses { get; set; }
    }
}
