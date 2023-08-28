using Application.Common.Persistence;
using Domain.PersonEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    public class PersonRepository : RepositoryBase<Person>,IPersonRepository
    {
        public PersonRepository(ApplicationDBContext dbContext) : base(dbContext)
        {

        }

       
    }
}
