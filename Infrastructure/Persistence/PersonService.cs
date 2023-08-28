using Application.Common.Persistence;
using Domain.PersonEntity;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class PersonService : IPersonService
    {

        private readonly IUnitOfWork _unitOfWork;

        public PersonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool CreatePerson(Person person)
        {
            
            _unitOfWork.PersonRepository.Create(person);
            return _unitOfWork.Save();

        }

        public bool DeletePerson(Guid  id)
        {
            var personEntity = GetPersonById(id);
            if (personEntity == null)
            {
                throw new Exception("Person doesn't exist");
            }
            _unitOfWork.PersonRepository.Delete(personEntity);
            return _unitOfWork.Save();
        }

        public List<Person> GetAllPersons()
        {
            return _unitOfWork.PersonRepository.FindAll().Include(x => x.childs)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public Person GetPersonById(Guid id)
        {
            var result = _unitOfWork.PersonRepository.FindByCondition(person => person.Id.Equals(id))
                 .FirstOrDefault();
            return result;
        }

        public bool UpdatePerson(Person person)
        {
            var personEntity = GetPersonById(person.Id);
            if (personEntity == null)
            {
                throw new Exception("Person doesn't exist");
            }
            _unitOfWork.PersonRepository.Update(person);
            return _unitOfWork.Save();
        }
    }
}
