using System;
using System.Collections.Generic;
using PhoneBookModels;

namespace PhoneBookInterfaces
{
	public interface IPhoneBookEntryValidators
	{
		List<ValidationError> ValidateAll(string Name, string PhoneNumber, long PhonebookId);
		List<ValidationError> ValidateName(string Name, List<ValidationError> Errors = null);
		List<ValidationError> ValidatePhoneNumber(string PhoneNumber, List<ValidationError> Errors = null);
	}
}
