using System;
using System.Collections.Generic;
using System.Linq;
using PhoneBookModels;
using PhoneBookInterfaces;

namespace PhoneBookBusiness
{
	public class PhoneBookEntryValidators: IPhoneBookEntryValidators
	{
		public List<ValidationError> ValidateAll(string Name, string PhoneNumber, long PhonebookId)
		{
			var errors = ValidateName(Name);
			ValidatePhoneNumber(PhoneNumber, errors);
			return errors;
		}

		public List<ValidationError> ValidateName(string Name, List<ValidationError> Errors = null) {

			if (Errors == null) Errors = new List<ValidationError>();

			if (Name.Length > 255) Errors.Add(new ValidationError { ID = (int)ValidationErrorEnum.LengthIncorrect, Name = "Length Incorrect", Description = "Phonebook name cannot be longer than 255 characters" });
			if (!Name.All(x=> char.IsLetterOrDigit(x) || x == ' ')) Errors.Add(new ValidationError { ID = (int)ValidationErrorEnum.InvalidCharacters, Name = "Invalid Characters", Description = "Phonebook name can only contain Alphabet letters (including international letters) numbers and spaces.  It can't contain special characters." });

			return Errors;
		}

		public List<ValidationError> ValidatePhoneNumber(string PhoneNumber, List<ValidationError> Errors = null)
		{

			if (Errors == null) Errors = new List<ValidationError>();

			string trimPhoneNumber = PhoneNumber.Replace(" ", "");
			
			if (trimPhoneNumber.Length != 10 && trimPhoneNumber.Length != 12) Errors.Add(new ValidationError { ID = (int)ValidationErrorEnum.LengthIncorrect, Name = "Length Incorrect", Description = "Phone numbers can only be 10 characters for local phone numbers E.G 083 123 4567 or 12 characters for international numbers E.G +27 83 123 4567 . ( not counting spaces)" });			
			if (!PhoneNumber.All(x => (x >= '0' && x <= '9') || x == ' ' || x == '+')) Errors.Add(new ValidationError { ID = (int)ValidationErrorEnum.InvalidCharacters, Name = "Invalid Characters", Description = "Phone numbers can only contain numbers, spaces and a + in the beginning of a international number." });
			int PlusLocation = trimPhoneNumber.LastIndexOf('+');
			if(!(PlusLocation == -1 || PlusLocation == 0 )) Errors.Add(new ValidationError { ID = (int)ValidationErrorEnum.InvalidCharacters, Name = "Invalid Characters", Description = "Phone numbers can only have a + in the beginning of a international number E.G +27 83 123 4567" });

			return Errors;
		}
	}
}
