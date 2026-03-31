using Microsoft.EntityFrameworkCore;


namespace ILearnSmartProject.Models
{
    public class LearnSmartContext : DbContext
    {
        public LearnSmartContext(DbContextOptions options) : base(options)
        {

        }

        public  DbSet<Users> Users { get; set; }

        public  DbSet<UsersType> UsersTypes { get; set; }


    }
}
