using System;
using System.Collections.Generic;
using System.Linq;
using ServiceLogic;
using ServiceLogic.UserEntity;
using WcfUserServiceLibrary.Interfaces;

namespace WcfUserServiceLibrary.Infostructure
{
    public static class Mapper
    {
        public static User ToUser(this UserDataContract userDC)
        {
            return new User()
            {
                Id = userDC.Id,
                FirstName = userDC.FirstName,
                LastName = userDC.LastName,
                DateBirth = userDC.DateOfBirth,
                Age = DateTime.Now.Year - userDC.DateOfBirth.Year,
                Gender = userDC.UserGender,
                Visa = userDC.UserVisa.ToVisa().ToArray()
            };
        }

        public static IEnumerable<Visa> ToVisa(this VisaDataContract[] visa)
        {
            foreach (var elem in visa)
            {
                yield return new Visa()
                {
                    Country = elem.Country,
                    Start = elem.Start,
                    End = elem.End
                };
            }

        }
    }
}