namespace ILearnSmartProject.Models
{
    public class Users
    {
     
        public int Id { get; set; }

        public  string FirstName { get; set; }

        public  string LastName { get; set; }
        public  string EmailAddress { get; set; }

        public  string Password { get; set; }

        public  DateTime CreationDate { get; set; }

        public  DateTime UpdateDate { get; set; }

        public  bool IsActive { get; set; }

        public  bool IsDeleted { get; set; }
        public Users(string firstName, string lastName, string emailAddress, string password, DateTime creationDate, DateTime updateDate, bool isActive, bool isDeleted)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            Password = password;
            CreationDate = creationDate;
            UpdateDate = updateDate;
            IsActive = isActive;
            IsDeleted = isDeleted;
        }



    }
}
