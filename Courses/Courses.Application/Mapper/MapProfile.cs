using AutoMapper;
using Courses.DataTransferObjects.Requests;
using Courses.DataTransferObjects.Responses;
using Courses.Entities;

namespace Courses.Application.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Course, CourseSummaryResponse>();
            CreateMap<CreateNewCourseRequest, Course>();
            CreateMap<UpdateCourseRequest, Course>();
        }
    }
}
