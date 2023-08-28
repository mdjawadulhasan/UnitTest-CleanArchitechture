using Application.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    public class UnitofWork : IUnitOfWork
    {
        public ApplicationDBContext _dbContext;
        private IPersonRepository _personRepository;
        private IChildRepository _childRepository;
        public UnitofWork(ApplicationDBContext repoContext)
        {
            _dbContext = repoContext;
        }
        public IPersonRepository PersonRepository
        {
            get
            {
                if (_personRepository == null)
                {
                    _personRepository = new PersonRepository(_dbContext);
                }
                return _personRepository;
            }
        }


        public IChildRepository ChildRepository
        {
            get
            {
                if (_childRepository == null)
                {
                    _childRepository = new ChildRepository(_dbContext);
                }
                return _childRepository;
            }
        }

        public bool Save()
        {
          return  _dbContext.SaveChanges()>0;
        }


    }
}
