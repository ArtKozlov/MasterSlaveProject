using System;
using System.Reflection;
using ServiceLogic.UserEntity;

namespace ServiceLogic.Validation
{
    public class Validator
    {
        public static bool StringIsValid(User user)
        {

            foreach (FieldInfo elem in typeof(User).GetFields())
            {
               
                foreach(Attribute attr in elem.GetCustomAttributes(false))
                {
                    var strAttr = attr as StringValidatorAttribute;

                    if (!ReferenceEquals(strAttr, null))
                    {
                        if (elem.GetValue(user).ToString().Length >= strAttr.Length ||
                            elem.GetValue(user).ToString().Length <= strAttr.MinLength)
                            return false;
                    }
                }

            }
            foreach (PropertyInfo elem in typeof(User).GetProperties())
            {
                foreach (Attribute attr in elem.GetCustomAttributes(false))
                {
                    var strAttr = attr as StringValidatorAttribute;

                    if (!ReferenceEquals(strAttr, null))
                    {
                        if (elem.GetValue(user).ToString().Length >= strAttr.Length ||
                            elem.GetValue(user).ToString().Length <= strAttr.MinLength)
                            return false;
                    }
                }

            }
            return true;
        }

        public static bool IntIsValid(User user)
        {

            foreach (FieldInfo elem in typeof(User).GetFields())
            {
                foreach (Attribute attr in elem.GetCustomAttributes(false))
                {
                    var intAttr = attr as IntValidatorAttribute;

                    if (!ReferenceEquals(intAttr, null))
                    {
                        if ((int)elem.GetValue(user) <= intAttr.Min || (int)elem.GetValue(user) >= intAttr.Max)
                            return false;
                    }
                }

            }
            foreach (PropertyInfo elem in typeof(User).GetProperties())
            {
                foreach (Attribute attr in elem.GetCustomAttributes(false))
                {
                    var intAttr = attr as IntValidatorAttribute;

                    if (!ReferenceEquals(intAttr, null))
                    {
                        if ((int)elem.GetValue(user) <= intAttr.Min || (int)elem.GetValue(user) >= intAttr.Max)
                            return false;
                    }
                }

            }
            return true;
        }

        public static bool UserIsValid(User user)
        {
            if (IntIsValid(user) && StringIsValid(user))
                return true;
            return false;
        }
    }
}
