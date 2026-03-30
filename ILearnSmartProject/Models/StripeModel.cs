namespace ILearnSmartProject.Models
{
    public class StripeModel
    {
        public required string SecretKey { get; set; }
        public required string PublishableKey { get; set; }

        public required string WebhookSecret { get; set; }
    }
}
