using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneBookModels;

namespace PhoneBookInterfaces
{
	public interface IPhonebookRepo
	{
		Task<PhoneBook> CreatePhonebookAsync(string Name);
		Task<PhoneBook> GetPhoneBookByIDAsync(int PhoneBookID);
		Task<PhoneBook> GetPhoneBookByNameAsync(string Name);
		Task<PhoneBook> GetPhoneBookByIDWithEntriesAsync(int PhoneBookID);
		Task<PhoneBook> GetPhoneBookByNameWithEntriesAsync(string Name);
		Task<PhoneBookEntry> CreatePhonebookEntryAsync(int PhoneBookID, string Name, string PhoneNumber);
		Task<PhoneBookEntry> EditPhonebookEntryAsync(PhoneBookEntry EditedPhoneBookEntry);
		int DeletePhonebookEntry(int PhoneBookEntryId);
		Task<List<PhoneBookEntry>> GetEntriesForPhoneBookAsync(int PhoneBookID);
		Task<PhoneBookEntry> GetPhonebookEntryByIDAsync(int EntryID);
		Task<List<PhoneBookEntry>> FindPhonebookEntriesByNameAsync(int PhoneBookID, string NamePart);
	}
}
