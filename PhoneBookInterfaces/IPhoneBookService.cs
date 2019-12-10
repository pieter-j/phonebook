using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneBookModels;

namespace PhoneBookInterfaces
{
	public interface IPhoneBookService
	{
		Task<(PhoneBook, List<ValidationError>)> CreatePhoneBookAsync(string Name);
		Task<(PhoneBookEntry, List<ValidationError>)> CreatePhonebookEntryAsync(int PhoneBookID, string Name, string PhoneNumber);
		Task<(PhoneBookEntry, List<ValidationError>)> EditPhonebookEntryAsync(PhoneBookEntry EditedPhoneBookEntry);
		Task<int> DeletePhonebookEntryAsync(int PhoneBookEntryId);
		Task<PhoneBook> GetPhoneBookAsync(string Name);
		Task<List<PhoneBookEntry>> GetPhoneBookEntriesAsync(int PhonebookId);
		Task<List<PhoneBookEntry>> FindPhonebookEntriesByNameAsync(int PhonebookId, string NamePart);
	}
}
