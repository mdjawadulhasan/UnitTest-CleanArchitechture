using Domain.PersonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Persistence
{
    public interface IPersonService
    {
        public Task<IReadOnlyList<Person>> GetAllPersons();
        Person GetPersonById(Guid id);
        bool CreatePerson(Person person);
        bool UpdatePerson(Person person);
        bool DeletePerson(Guid  id);
    }
}
