using ServiceLogic.Validation;
using System;
using System.Collections.Generic;

namespace ServiceLogic
{
    public enum Gender { Male, Female }

    [Serializable]
    public class User : IEqualityComparer<User>, IEquatable<User>
    {
        public User() { }

        public User(string firstName, string lastName, DateTime dateBirth, Gender gender)
        {
            FirstName = firstName;
            LastName = lastName;
            DateBirth = dateBirth;
            Gender = gender;
            Age = DateTime.Now.Year - dateBirth.Year;
        }

        public int Id { get;set; }

        [StringValidator(1,40)]
        public string FirstName { get; set; }

        [StringValidator(1, 40)]
        public string LastName { get; set; }

        [IntValidator(1, 99)]
        public int Age { get; set; }

        public DateTime DateBirth { get; set; }
        public Visa[] Visa { get; set; }
        public Gender Gender { get; set; }

        public bool Equals(User lhr, User rhs)
        {
            if (ReferenceEquals(lhr, rhs))
                return true;
            if (lhr.Id == rhs.Id && lhr.FirstName == rhs.FirstName
                && lhr.LastName == rhs.LastName && lhr.DateBirth == rhs.DateBirth
                && lhr.Gender == rhs.Gender)
                return true;
            return false;
        }

        public int GetHashCode(User user)
        {
            return user.FirstName.GetHashCode() + user.LastName.GetHashCode() + user.DateBirth.GetHashCode();
        }

        public bool Equals(User otherUser)
        {
            if (otherUser == null)
            {
                return false;
            }

            return Equals(this, otherUser);
        }
    }
}
