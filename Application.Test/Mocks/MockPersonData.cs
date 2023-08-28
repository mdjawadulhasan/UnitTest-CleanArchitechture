using Domain.PersonEntity;
using System;
using System.Collections.Generic;

namespace Test.Mocks
{
    public class MockPersonData
    {
        public static List<Person> GenerateMockPersonData()
        {
            var persons = new List<Person>()
            {
                new Person()
                {
                    Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                    Name = "Jawad",
                    DateOfBirth = new DateTime(2001, 01, 25),
                    Address="Banasree"
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
