using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;


namespace ILearnSmartProject.Models
{
    public class Course
    {

        public int Id { get; set; }

        public required string CourseTitle { get; set; }

        public required string CourseDescription { get; set; }

        // storing the price of the course
        [Column(TypeName = "decimal(18, 2)")]
        public decimal CoursePrice { get; set; } = 0;   
        
        public required bool CourseEnabled { get; set; }
        
        public required string BlobName { get; set; }


        // handling the video uploaded by the user
        [NotMapped]
        public required IFormFile CourseVideoFile { get; set; }
             

    }
}
