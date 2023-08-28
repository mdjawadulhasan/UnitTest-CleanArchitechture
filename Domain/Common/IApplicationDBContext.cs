using Domain;
using Domain.PersonEntity;
using Microsoft.EntityFrameworkCore;

namespace Domain.Common
{
    public interface IApplicationDBContext
    {
        DbSet<Person> Persons { get; set; }
       
    }
}
