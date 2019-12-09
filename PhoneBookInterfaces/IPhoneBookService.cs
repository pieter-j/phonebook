using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneBookModels;

namespace PhoneBookInterfaces
{
	public interface IPhoneBookService
	{
		Task<(PhoneBook, List<ValidationError>)> CreatePhoneBookAsync(string Name);
		Task<(PhoneBookEntry, List<ValidationError>)> CreatePhonebookEntryAsync(long PhoneBookID, string Name, string PhoneNumber);
		Task<PhoneBook> GetPhoneBookAsync(string Name);
		Task<IEnumerable<PhoneBookEntry>> GetPhoneBookEntriesAsync(long PhonebookId);
		Task<IEnumerable<PhoneBookEntry>> FindPhonebookEntriesByNameAsync(long PhonebookId, string NamePart);
	}
}
