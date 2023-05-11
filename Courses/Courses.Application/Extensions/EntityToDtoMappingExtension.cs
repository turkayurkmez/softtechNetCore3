using AutoMapper;
using Courses.DataTransferObjects.Requests;
using Courses.Entities;

namespace Courses.Application.Extensions
{
    public static class EntityToDtoMappingExtension
    {
        public static T ConvertToDto<T>(this IEnumerable<Course> course, IMapper mapper)
        {
            return mapper.Map<T>(course);

        }
        public static T ConvertToDto<T>(this Course course, IMapper mapper)
        {
            return mapper.Map<T>(course);
        }

        public static Course ConvertToEntity(this CreateNewCourseRequest request, IMapper mapper)
        {
            return mapper.Map<Course>(request);
        }
        public static Course ConvertToEntity(this UpdateCourseRequest request, IMapper mapper)
        {
            return mapper.Map<Course>(request);
        }
    }
}
