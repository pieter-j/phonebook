using PhoneBookModels;
using PhoneBookData.Repo;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneBookInterfaces;

namespace PhoneBookBusiness
{
	public class PhoneBookService: IPhoneBookService
	{
		protected IPhonebookRepo Repo;
		protected IPhoneBookValidators PhoneBookValidator;
		protected IPhoneBookEntryValidators PhoneBookEntryValidator;

		public PhoneBookService(IPhonebookRepo Repo, IPhoneBookValidators PhoneBookValidator, IPhoneBookEntryValidators PhoneBookEntryValidator)
		{
			this.Repo = Repo;
			this.PhoneBookValidator = PhoneBookValidator;
			this.PhoneBookEntryValidator = PhoneBookEntryValidator;
		}

		public async Task<(PhoneBook, List< ValidationError>)> CreatePhoneBookAsync(string Name)
		{
			var ValidationErrors = PhoneBookValidator.ValidateName(Name);
			if (ValidationErrors.Count == 0)
			{
				return (await Repo.CreatePhonebookAsync(Name), null);
			} else
			{
				return (null, ValidationErrors);
			}
		}

		public async Task<(PhoneBookEntry, List<ValidationError>)> CreatePhonebookEntryAsync(int PhoneBookID, string Name, string PhoneNumber)
		{
			var ValidationErrors = PhoneBookEntryValidator.ValidateAll(Name,PhoneNumber, PhoneBookID);
			if (ValidationErrors.Count == 0)
			{
				return (await Repo.CreatePhonebookEntryAsync(PhoneBookID, Name, PhoneNumber), null) ;
			}
			else
			{
				return (null, ValidationErrors);
			}
		}

		public async Task<(PhoneBookEntry, List<ValidationError>)> EditPhonebookEntryAsync(PhoneBookEntry EditedPhoneBookEntry)
		{
			var ValidationErrors = PhoneBookEntryValidator.ValidateAll(EditedPhoneBookEntry.Name, EditedPhoneBookEntry.PhoneNumber, EditedPhoneBookEntry.PhoneBookId);
			if (ValidationErrors.Count == 0)
			{
				return (await Repo.EditPhonebookEntryAsync(EditedPhoneBookEntry), null);
			}
			else
			{
				return (null, ValidationErrors);
			}
		}

		public async Task<int> DeletePhonebookEntryAsync(int PhoneBookEntryId)
		{
			return Repo.DeletePhonebookEntry(PhoneBookEntryId);
		}

		public async Task<PhoneBook> GetPhoneBookAsync(string Name)
		{
			return await Repo.GetPhoneBookByNameWithEntriesAsync(Name);
		}

		public async Task<List< PhoneBookEntry>> GetPhoneBookEntriesAsync(int PhonebookId)
		{
			return await Repo.GetEntriesForPhoneBookAsync(PhonebookId);
		}
		
		public async Task<List<PhoneBookEntry>> FindPhonebookEntriesByNameAsync(int PhonebookId, string NamePart)
		{
			return await Repo.FindPhonebookEntriesByNameAsync(PhonebookId, NamePart);
		}
	}
}
