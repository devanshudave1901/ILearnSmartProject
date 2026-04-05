using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
namespace ILearnSmartProject.Models
{
    public class PDF_Generation
    {
        private readonly LearnSmartContext _learnSmartContext;
        public PDF_Generation(LearnSmartContext learnSmartContext)
        {
            _learnSmartContext = learnSmartContext;
        }
      
        public byte[] GeneratePDF(string courseId, string userId)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            // gather the necessary data for the PDF, such as course details and user information

            var courseData = _learnSmartContext.Courses.Where(c => c.Id == int.Parse(courseId)).FirstOrDefault();
            var userData = _learnSmartContext.Users.Where(u => u.Id == int.Parse(userId)).FirstOrDefault();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.Content().Column(column =>

                    {
                        column.Spacing(25);

                        column.Item()
                        .BackgroundLinearGradient(0, [Colors.Red.Lighten2, Colors.Blue.Lighten2])
                        .AspectRatio(2);
                        column.Item().Text("Course Finished").FontSize(20);
                        column.Item().Image("https://ilearnsmartproject-e3b7hfa9e7akexg4.canadacentral-01.azurewebsites.net/images/completionIcon.png");
                        column.Item().Text("This certificate proves that this student ." + (userData.FirstName + " " + userData.LastName) + " has successfully completed the " + courseData.CourseTitle).FontSize(14);
                        column.Item().Text("Congratulations! You've turned your goals into results—keep building on this success");
                        column.Item().Text("Issued by ISmart Learn @2026").FontSize(20);

                    });
                });
            });
            return document.GeneratePdf();
        }
    }

}
