using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLogic
{
    public class User
    {
        public User() { }

        public User(string firstName, string lastName, DateTime dateBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            DateBirth = dateBirth;
            Age = DateTime.Now.Year - dateBirth.Year;
        }

        public int Id { get;set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateBirth { get; set; }
        public int Age { get; set; }

    }
}
