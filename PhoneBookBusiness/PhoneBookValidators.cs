using System;
using System.Collections.Generic;
using System.Linq;
using PhoneBookModels;
using PhoneBookInterfaces;

namespace PhoneBookBusiness
{
	public class PhoneBookValidators: IPhoneBookValidators
	{
		public List<ValidationError> ValidateAll(string Name)
		{
			return ValidateName(Name);
		}
		public List<ValidationError> ValidateName(string Name, List<ValidationError> Errors = null) {

			if (Errors == null) Errors = new List<ValidationError>();

			if (Name.Length > 255) Errors.Add(new ValidationError { ID = (int)ValidationErrorEnum.LengthIncorrect, Name = "Length Incorrect", Description = "Phonebook name cannot be longer than 255 characters" });
			if (Name.All(x=> char.IsLetterOrDigit(x) || x == ' ')) Errors.Add(new ValidationError { ID = (int)ValidationErrorEnum.InvalidCharacters, Name = "Invalid Characters", Description = "Phonebook name can only contain Alphabet letters (including international letters) numbers and spaces.  It can't contain any special characters." });

			return Errors;
		}
	}
}
