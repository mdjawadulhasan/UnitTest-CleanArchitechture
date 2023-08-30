using Domain.ChildEntity;
using Domain.PersonEntity;
using System;
using System.Collections.Generic;

namespace Test.Mocks
{
    public class MockPersonData
    {
        public static List<Person> GetMockPersonData()
        {
            var persons = new List<Person>()
            {
                new Person()
                {
                    Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                    Name = "Jawad",
                    DateOfBirth = new DateTime(2001, 01, 25),
                    Address="Banasree",
                    childs=new List<Child>
                   {
                       new Child(){Name="Jawad Jnr",DateOfBirth=DateTime.Now.AddYears(-1)},
                       new Child(){Name="Jawad Jnr2",DateOfBirth=DateTime.Now.AddYears(-1)},
                   }
                },
                new Person()
                {
                    Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950b"),
                    Name = "Ishan",
                    DateOfBirth = new DateTime(2001, 01, 25)
                },
                new Person()
                {
                    Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950a"),
                    Name = "Hasan",
                    DateOfBirth = new DateTime(2001, 01, 25)
                }
            };

            return persons;
        }
    }
}
