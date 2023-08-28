using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Persistence
{
    public interface IUnitOfWork
    {
        IPersonRepository PersonRepository { get; }
        IChildRepository ChildRepository { get; }
        bool Save();
    }
}
