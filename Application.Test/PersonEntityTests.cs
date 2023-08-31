using Application.Common.Persistence;
using Domain.ChildEntity;
using Domain.PersonEntity;
using FluentAssertions;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Test.Mocks;

namespace Test
{
    public class PersonEntityTests
    {

        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IPersonRepository> _mockpersonRepository;
        private PersonService _personService;

        public PersonEntityTests()
        {

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mockpersonRepository = new Mock<IPersonRepository>();

        }


        [Fact]
        public async void PersonService_GetAllPersons_ReturnListOfPersons()
        {

            var persons = MockPersonData.GetMockPersonData().AsQueryable().BuildMock();


            _mockpersonRepository.Setup(x => x.Query()).Returns(persons);



            _unitOfWorkMock.Setup(m => m.PersonRepository).Returns(_mockpersonRepository.Object);
            _personService = new PersonService(_unitOfWorkMock.Object);


            var result = await _personService.GetAllPersons();
            var specificPerson = result.Where(x => x.Name == "Jawad").FirstOrDefault();

            result.Should().NotBeNull();
            result.Should().BeOfType<List<Person>>();
            result.Should().HaveCount(3);
            specificPerson.childs.Should().NotBeNull();
            specificPerson.childs.Should().HaveCount(2);


        }
    }
}
