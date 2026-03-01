namespace ILearnSmartProject.Models
{
    public class Users
    {
        public int Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }
        public required string EmailAddress { get; set; }
    }
}
