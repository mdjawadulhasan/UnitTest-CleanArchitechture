using Application.Common.Persistence;
using Domain.ChildEntity;
using Domain.PersonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    public class ChildRepository : RepositoryBase<Child>, IChildRepository
    {
        public ChildRepository(ApplicationDBContext context) : base(context)
        {
        }

       
    }
}
