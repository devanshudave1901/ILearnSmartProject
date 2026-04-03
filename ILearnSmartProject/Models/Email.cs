using ILearnSmartProject.Interfaces;

namespace ILearnSmartProject.Models
{
    public class Email : IObserver
    {
        public void Notify(string message)
        {
            // Simulate sending an email by printing the message to the console
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var emailContent = $"[Email Notification] {timestamp}: {message}";

            Console.WriteLine(emailContent);


        }
    }
}
