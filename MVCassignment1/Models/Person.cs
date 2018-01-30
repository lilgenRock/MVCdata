using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCassignment2.Models
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string City { get; set; }


        public static List<Person> AddPerson(List<Person> people, string Name, string PhoneNumber, string City)
        {
            int newId = 0;
            if (Name != "" && Name != null)
            {
                if (people.Count > 0)        // if people exist in list find the largest id and give new person that value + 1
                {
                    newId = people.Max(p => p.Id) + 1;
                }
                people.Add(new Person
                {
                    Id = newId,
                    Name = Name,
                    PhoneNumber = PhoneNumber,
                    City = City
                });
            }
            return people;
        }
    }
}