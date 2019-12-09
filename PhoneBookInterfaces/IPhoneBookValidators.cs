using System.Collections.Generic;
using PhoneBookModels;

namespace PhoneBookInterfaces
{
	public interface IPhoneBookValidators
	{
		List<ValidationError> ValidateAll(string Name);
		List<ValidationError> ValidateName(string Name, List<ValidationError> Errors = null);

	}
}
