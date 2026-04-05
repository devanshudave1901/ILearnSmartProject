using ILearnSmartProject.Models;

namespace ILearnSmartProject.Payment.StripeManager
{
    public interface ICheckOutSession
    {
        public Task<List<string>> CreateCheckOutSession(string courseId, string successUrl, string cancelUrl);
        public Task<string> ConfirmPayment(string sessionId);

    }
}
