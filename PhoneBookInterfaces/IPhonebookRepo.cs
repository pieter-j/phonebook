using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneBookModels;

namespace PhoneBookInterfaces
{
	public interface IPhonebookRepo
	{
		Task<PhoneBook> CreatePhonebookAsync(string Name);
		Task<PhoneBook> GetPhoneBookByIDAsync(long PhoneBookID);
		Task<PhoneBook> GetPhoneBookByNameAsync(string Name);
		Task<PhoneBook> GetPhoneBookByIDWithEntriesAsync(long PhoneBookID);
		Task<PhoneBook> GetPhoneBookByNameWithEntriesAsync(string Name);
		Task<PhoneBookEntry> CreatePhonebookEntryAsync(long PhoneBookID, string Name, string PhoneNumber);
		Task<IEnumerable<PhoneBookEntry>> GetEntriesForPhoneBookAsync(long PhoneBookID);
		Task<PhoneBookEntry> GetPhonebookEntryByIDAsync(long EntryID);
		Task<IEnumerable<PhoneBookEntry>> FindPhonebookEntriesByNameAsync(long PhoneBookID, string NamePart);
	}
}
