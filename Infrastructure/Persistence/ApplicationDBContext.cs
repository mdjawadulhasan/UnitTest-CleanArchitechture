using Domain.ChildEntity;
using Domain.Common;
using Domain.PersonEntity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDBContext : DbContext, IApplicationDBContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
         : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Child>Childs { get; set; }
    }
}
