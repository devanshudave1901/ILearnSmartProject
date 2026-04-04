using ILearnSmartProject.Interfaces;
using ILearnSmartProject.Services;


namespace ILearnSmartProject.Models
{
    public class Email : IObserver
    {


        private EmailAppService _emailAppService;
       

        public Email(EmailAppService emailAppService)
        {
            _emailAppService = emailAppService;
        }

        public async Task Notify(string message, string emailAddress)
        {
            var email = await _emailAppService.SendEmailAsync(message, emailAddress);
            

        }
    }
}
