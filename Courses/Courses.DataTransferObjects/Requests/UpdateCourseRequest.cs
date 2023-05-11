using System.ComponentModel.DataAnnotations;

namespace Courses.DataTransferObjects.Requests
{
    public class UpdateCourseRequest
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Kurs adı boş olamaz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Kurs açıklaması boş olamaz")]
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? Price { get; set; }
        public int? TotalHours { get; set; }
        public int? CategoryId { get; set; }
        public string CourseImage { get; set; } = "https://loremflickr.com/320/240";
    }
}
