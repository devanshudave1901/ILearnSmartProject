using ILearnSmartProject.Models;

namespace ILearnSmartProject.Payment.StripeManager
{
    public interface ICheckOutSession
    {
        public Task<List<string>> CreateCheckOutSession(string priceId, string successUrl, string cancelUrl);
    }
}
