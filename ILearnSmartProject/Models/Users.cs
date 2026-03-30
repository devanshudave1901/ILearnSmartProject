namespace ILearnSmartProject.Models
{
    public class Users
    {
        public int Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }
        public required string EmailAddress { get; set; }

        public required string Password { get; set; }

        public required DateTime CreationDate { get; set; }

        public required DateTime UpdateDate { get; set; }

        public required bool IsActive { get; set; }

        public required bool IsDeleted { get; set; }


    }
}
