using Application.Common.Persistence;
using Domain.PersonEntity;
using FluentAssertions;
using Infrastructure.Persistence;
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

        [Theory]
        [InlineData(null, null, null, false)]
        [InlineData(null, "TestAddress", null, false)]
        [InlineData(null, null, "06/04/1994", false)]
        [InlineData("TestName", null, null, false)]
        [InlineData(null, "TestAddress", "06/04/1994", false)]
        [InlineData("TestName", null, "06/04/1994", false)]
        [InlineData("abcdefghijklmnopqrstuvwxyz", "TestAdress", "06/04/1994", false)]
        public void PersonService_CreatePerson_InvalidInput(string? name, string? address, string? dateOfBirth, bool isValid)
        {
            var person = new Person()
            {
                Address = address,
                Name = name,
                DateOfBirth = dateOfBirth is null ? DateTime.MinValue : DateTime.Parse(dateOfBirth)
            };

            isValid.Should().Be(ValidateModel(person));

        }

        [Theory]
        [InlineData("Jawad", "Banasree", "01/25/2001", true)]
        public void PersonService_CreatePerson_ReturnTrueWithValidInput(string? name, string? address, string? dateOfBirth, bool isValid)
        {
            var person = new Person()
            {
                Address = address,
                Name = name,
                DateOfBirth = dateOfBirth is null ? DateTime.MinValue : DateTime.Parse(dateOfBirth)

            };


            isValid.Should().Be(ValidateModel(person));

            _mockpersonRepository.Setup(m => m.Create(It.Is<Person>(x =>
            x.Name == person.Name &&
            x.Address == person.Address &&
            x.DateOfBirth == person.DateOfBirth
            ))).Verifiable();

            _unitOfWorkMock.Setup(m => m.PersonRepository).Returns(_mockpersonRepository.Object);
            _unitOfWorkMock.Setup(x => x.Save()).Returns(true);

            _personService = new PersonService(_unitOfWorkMock.Object);
            var result = _personService.CreatePerson(person);


            result.Should().Be(true);

        }


        [Theory]
        [InlineData("Jawad", "Banasree", "01/25/2001", true)]
        public void PersonService_UpdatePerson_ReturnTrueWithValidInput(string? name, string? address, string? dateOfBirth, bool isValid)
        {
            var person = new Person()
            {
                Address = address,
                Name = name,
                DateOfBirth = dateOfBirth is null ? DateTime.MinValue : DateTime.Parse(dateOfBirth),
                Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e")

            };


            isValid.Should().Be(ValidateModel(person));


            var persons = MockPersonData.GenerateMockPersonData();
            IQueryable<Person> queryablePersons = persons.AsQueryable();

            _mockpersonRepository.Setup(m => m.FindByCondition(It.IsAny<Expression<Func<Person, bool>>>()))
            .Returns((Expression<Func<Person, bool>> expression) =>
                queryablePersons.Where(expression));


            _mockpersonRepository.Setup(m => m.Update(It.Is<Person>(x =>
            x.Name == person.Name &&
            x.Address == person.Address &&
            x.DateOfBirth == person.DateOfBirth
            ))).Verifiable();

            _unitOfWorkMock.Setup(m => m.PersonRepository).Returns(_mockpersonRepository.Object);
            _unitOfWorkMock.Setup(x => x.Save()).Returns(true);

            _personService = new PersonService(_unitOfWorkMock.Object);
            var result = _personService.UpdatePerson(person);


            result.Should().Be(true);

        }



        [Fact]
        public void PersonService_UpdatePerson_ThrowsExceptionWhenPersonNotFound()
        {
            var person = new Person()
            {
                Address = "NoWhere",
                Name = "ABC",
                DateOfBirth = DateTime.Now,
                Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-708677289500")

            };


            var persons = MockPersonData.GenerateMockPersonData();
            IQueryable<Person> queryablePersons = persons.AsQueryable();

            _mockpersonRepository.Setup(m => m.FindByCondition(It.IsAny<Expression<Func<Person, bool>>>()))
            .Returns((Expression<Func<Person, bool>> expression) =>
                queryablePersons.Where(expression));

            _unitOfWorkMock.Setup(m => m.PersonRepository).Returns(_mockpersonRepository.Object);

            _personService = new PersonService(_unitOfWorkMock.Object);

            var action = () => _personService.UpdatePerson(person);
            action.Should().Throw<Exception>().WithMessage("Person doesn't exist");

        }


        [Theory]
        [InlineData("0f8fad5b-d9cb-469f-a165-70867728950e")]
        [InlineData("0f8fad5b-d9cb-469f-a165-70867728950a")]
        public void PersonService_GetPersonById_ReturnValidPerson(string id)
        {

            var persons = MockPersonData.GenerateMockPersonData();
            IQueryable<Person> queryablePersons = persons.AsQueryable();

            _mockpersonRepository.Setup(m => m.FindByCondition(It.IsAny<Expression<Func<Person, bool>>>()))
            .Returns((Expression<Func<Person, bool>> expression) =>
                queryablePersons.Where(expression));
            _unitOfWorkMock.Setup(m => m.PersonRepository).Returns(_mockpersonRepository.Object);
            _personService = new PersonService(_unitOfWorkMock.Object);

            var result = _personService.GetPersonById(Guid.Parse(id));

            result.Should().NotBeNull();
            result.Should().BeOfType<Person>();
            result.Id.Should().Be(id);
        }



        [Fact]
        public void PersonService_GetAllPersons_ReturnListOfPersons()
        {

            var persons = MockPersonData.GenerateMockPersonData();
            IQueryable<Person> queryablePersons = persons.AsQueryable();

            _mockpersonRepository.Setup(m => m.FindAll()).Returns(queryablePersons);
            _unitOfWorkMock.Setup(m => m.PersonRepository).Returns(_mockpersonRepository.Object);
            _personService = new PersonService(_unitOfWorkMock.Object);


            var result = _personService.GetAllPersons().ToList();


            result.Should().NotBeNull();
            result.Should().BeOfType<List<Person>>();
            result.Should().HaveCount(3);



        }



        [Fact]
        public void PersonService_DeletePerson_ReturnsTrueOnSuccessfulDelete()
        {

            var personIdToDelete = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e");

            var persons = MockPersonData.GenerateMockPersonData();
            IQueryable<Person> queryablePersons = persons.AsQueryable();

            _mockpersonRepository.Setup(m => m.FindByCondition(It.IsAny<Expression<Func<Person, bool>>>()))
                .Returns((Expression<Func<Person, bool>> expression) =>
                    queryablePersons.Where(expression));

            var personToDelete = persons.FirstOrDefault(p => p.Id == personIdToDelete);
            _mockpersonRepository.Setup(m => m.Delete(It.IsAny<Person>())).Callback<Person>(p => persons.Remove(p));
            _unitOfWorkMock.Setup(m => m.PersonRepository).Returns(_mockpersonRepository.Object);
            _unitOfWorkMock.Setup(x => x.Save()).Returns(true);


            _personService = new PersonService(_unitOfWorkMock.Object);


            var result = _personService.DeletePerson(personIdToDelete);


            result.Should().BeTrue();
            persons.Should().NotContain(personToDelete);
        }


        [Fact]
        public void PersonService_DeletePerson_ThrowsExceptionWhenPersonNotFound()
        {

            var personId = Guid.Parse("0f8fad5b-d9cb-469f-a165-708677289500");

            var persons = MockPersonData.GenerateMockPersonData();
            IQueryable<Person> queryablePersons = persons.AsQueryable();

            _mockpersonRepository.Setup(m => m.FindByCondition(It.IsAny<Expression<Func<Person, bool>>>()))
                .Returns((Expression<Func<Person, bool>> expression) =>
                    queryablePersons.Where(expression));

            _unitOfWorkMock.Setup(m => m.PersonRepository).Returns(_mockpersonRepository.Object);

            _personService = new PersonService(_unitOfWorkMock.Object);

            var action = () => _personService.DeletePerson(personId);
            action.Should().Throw<Exception>().WithMessage("Person doesn't exist");
        }



        private bool ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            return Validator.TryValidateObject(model, ctx, validationResults, true);
        }


    }
}
