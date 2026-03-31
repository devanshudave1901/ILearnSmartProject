namespace ILearnSmartProject.Models
{
    public class UsersType
    {

        public int Id { get; set; }

        public required string UserTypeName { get; set; }
        public DateTime CreationDate { get; set; }

        public DateTime UpdateDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
