using AutoMapper;
using Courses.Application.Extensions;
using Courses.DataOperations.Repositories;
using Courses.DataTransferObjects.Requests;
using Courses.DataTransferObjects.Responses;

namespace Courses.Application.Services
{
    //[Obsolete("Bunun yerine yeni FeaturedCourseService kullanın!")]
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repository;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> CreateNewCourse(CreateNewCourseRequest request)
        {
            var course = request.ConvertToEntity(_mapper);
            await _repository.Create(course);
            return course.Id;
        }

        public async Task<CourseSummaryResponse> GetCourse(int id)
        {
            var course = await _repository.GetAsync(id);
            return course.ConvertToDto<CourseSummaryResponse>(_mapper);

        }

        public async Task<IEnumerable<CourseSummaryResponse>> GetCoursesAsync()
        {
            var courses = await _repository.GetAllAsync();
            var responses = courses.ConvertToDto<IEnumerable<CourseSummaryResponse>>(_mapper);

            return responses;

            //eğer autoMapper kütüphanesi olmasaydı:
            //var responses = courses.Select(c => new CourseSummaryResponse
            //{
            //    Id = c.Id,
            //    CourseImage = c.CourseImage,
            //    Description = c.Description,
            //    Name = c.Name
            //});

        }

        public async Task<IEnumerable<CourseSummaryResponse>> SearchCourse(string name)
        {
            var courses = await _repository.SeachByName(name);
            var response = courses.ConvertToDto<IEnumerable<CourseSummaryResponse>>(_mapper);
            return response;
        }

        public async Task UpdateCourse(UpdateCourseRequest request)
        {
            var course = request.ConvertToEntity(_mapper);
            await _repository.Update(course);

        }
    }
}
