using System;

namespace ServiceLogic.Validation
{
    // Should be applied to properties and fields.
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class StringValidatorAttribute : Attribute
    {
        public int Length { get; set; }
        public int MinLength { get; set; }
        public StringValidatorAttribute(int minLength, int length)
        {
            MinLength = minLength;
            Length = length;
        }
    }
}
