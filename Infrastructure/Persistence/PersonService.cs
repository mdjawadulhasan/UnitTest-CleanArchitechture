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



        public async Task<IReadOnlyList<Person>> GetAllPersons()
        {
            return await _unitOfWork.PersonRepository
                        .Query()
                        .Include(x => x.childs)
                        .OrderBy(p => p.Name)
                        .ToListAsync();
        }








        public bool CreatePerson(Person person)
        {

            _unitOfWork.PersonRepository.Create(person);
            return _unitOfWork.Save();

        }



        public async Task<Person> GetPersonById(Guid id)
        {
            var result = await _unitOfWork.PersonRepository.FindByCondition(person => person.Id.Equals(id))
                 .FirstOrDefaultAsync();
            return result;



        }

        public bool UpdatePerson(Person person)
        {
            throw new NotImplementedException();
        }

        public bool DeletePerson(Guid id)
        {
            throw new NotImplementedException();
        }


    }
}
