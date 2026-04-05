using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;


namespace ILearnSmartProject.Models
{
    public class CourseCertificates
    {

        public int Id { get; set; }

        public Course Course { get; set; }

        public Users User { get; set; }

    }
}
