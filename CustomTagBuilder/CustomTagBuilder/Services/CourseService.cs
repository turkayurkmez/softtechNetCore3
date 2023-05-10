using CustomTagBuilder.Models;

namespace CustomTagBuilder.Services
{
    public class CourseService
    {

        private List<Course> courses = new List<Course>
        {
            new(){ Id=1, Name="İleri Asp.Net Core", Description="Middleware...."},
            new(){ Id=2, Name="Temel Asp.Net Core", Description="MVC API"},
            new(){ Id=3, Name="Design Patterns", Description="Middleware...."},
            new(){ Id=4, Name="Blazor", Description="Middleware...."},
            new(){ Id=5, Name="Angular", Description="Middleware...."},
            new(){ Id=6, Name="Fltter", Description="Middleware...."},
            new(){ Id=7, Name="X", Description="Middleware...."},
            new(){ Id=8, Name="Y", Description="MVC API"},
            new(){ Id=13, Name="Z", Description="Middleware...."},
            new(){ Id=14, Name="A", Description="Middleware...."},
            new(){ Id=15, Name="B", Description="Middleware...."},
            new(){ Id=16, Name="C", Description="Middleware...."},
        };
        public IEnumerable<Course> GetCourses()
        {
            return courses;
        }
    }
}
