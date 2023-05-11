using Courses.DataTransferObjects.Requests;
using Courses.DataTransferObjects.Responses;

namespace Courses.Application.Services
{
    public interface ICourseService
    {
        Task<int> CreateNewCourse(CreateNewCourseRequest request);
        Task UpdateCourse(UpdateCourseRequest request);

        Task<CourseSummaryResponse> GetCourse(int id);
        Task<IEnumerable<CourseSummaryResponse>> GetCoursesAsync();
        Task<IEnumerable<CourseSummaryResponse>> SearchCourse(string name);

    }
}
