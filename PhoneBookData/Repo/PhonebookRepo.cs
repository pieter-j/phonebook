using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using PhoneBookModels;
using PhoneBookInterfaces;

namespace PhoneBookData.Repo
{

	public class PhonebookRepo : IPhonebookRepo
	{
		protected PhoneContext PhoneDB;
		protected IUnitofWork Uow;

		public IUnitofWork UnitOfWork {
			get { return Uow; } 
		}

		public PhonebookRepo(PhoneContext db, IUnitofWork uow)
		{
			PhoneDB = db;
			Uow = uow;
		}

		public async Task<PhoneBook> CreatePhonebookAsync(string Name)
		{
			var phoneBook = new PhoneBook() { Name = Name };
			PhoneDB.Add(phoneBook);
			await Uow.SaveChangesAsync();
			return phoneBook;
		}

		public async Task<PhoneBook> GetPhoneBookByIDAsync(long PhoneBookID)
		{
			var phoneBook = await (from pb in PhoneDB.PhoneBooks where pb.ID == PhoneBookID select pb).FirstOrDefaultAsync();
			return phoneBook;
		}

		public async Task<PhoneBook> GetPhoneBookByNameAsync(string Name)
		{
			var phoneBook = await (from pb in PhoneDB.PhoneBooks where pb.Name == Name select pb).FirstOrDefaultAsync();
			return phoneBook;
		}

		public async Task<PhoneBook> GetPhoneBookByIDWithEntriesAsync(long PhoneBookID)
		{
			var phoneBook = await (from pb in PhoneDB.PhoneBooks where pb.ID == PhoneBookID select pb).Include(pb => pb.Entries).FirstOrDefaultAsync();
			return phoneBook;
		}

		public async Task<PhoneBook> GetPhoneBookByNameWithEntriesAsync(string Name)
		{
			var phoneBook = await (from pb in PhoneDB.PhoneBooks where pb.Name == Name select pb).Include(pb => pb.Entries).FirstOrDefaultAsync();
			return phoneBook;
		}

		public async Task<PhoneBookEntry> CreatePhonebookEntryAsync(long PhoneBookID, string Name, string PhoneNumber)
		{
			var phoneBook = await GetPhoneBookByIDAsync(PhoneBookID);
			if (phoneBook == null)
			{
				return null;
			}
			else
			{
				var phoneBookEntry = new PhoneBookEntry() { Name = Name, PhoneNumber = PhoneNumber };
				phoneBook.Entries.Append(phoneBookEntry);
				await Uow.SaveChangesAsync();
				return phoneBookEntry;
			}
		}

		public async Task<IEnumerable<PhoneBookEntry>> GetEntriesForPhoneBookAsync(long PhoneBookID)
		{
			var phoneBookEntries = await (from pbe in PhoneDB.PhoneBookEntries where pbe.PhoneBookID == PhoneBookID select pbe).ToListAsync();
			return phoneBookEntries;
		}

		public async Task<PhoneBookEntry> GetPhonebookEntryByIDAsync(long EntryID)
		{
			var phoneBookEntry = await (from pbe in PhoneDB.PhoneBookEntries where pbe.ID == EntryID select pbe).FirstOrDefaultAsync();
			return phoneBookEntry;
		}

		public async Task<IEnumerable<PhoneBookEntry>> FindPhonebookEntriesByNameAsync(long PhoneBookID, string NamePart)
		{
			var phoneBookEntries = await (from pbe in PhoneDB.PhoneBookEntries where pbe.PhoneBookID == PhoneBookID && pbe.Name.Contains(NamePart) select pbe).ToListAsync();
			return phoneBookEntries;
		}
	}
}
