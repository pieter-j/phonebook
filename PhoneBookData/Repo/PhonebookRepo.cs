using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using PhoneBookModels;
using PhoneBookInterfaces;
using System;

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

		public async Task<PhoneBook> GetPhoneBookByIDAsync(int PhoneBookID)
		{
			var phoneBook = await (from pb in PhoneDB.PhoneBooks where pb.Id == PhoneBookID select pb).FirstOrDefaultAsync();
			return phoneBook;
		}

		public async Task<PhoneBook> GetPhoneBookByNameAsync(string Name)
		{
			var phoneBook = await (from pb in PhoneDB.PhoneBooks where pb.Name == Name select pb).FirstOrDefaultAsync();
			return phoneBook;
		}

		public async Task<PhoneBook> GetPhoneBookByIDWithEntriesAsync(int PhoneBookID)
		{
			var phoneBook = await (from pb in PhoneDB.PhoneBooks where pb.Id == PhoneBookID select pb).Include(pb => pb.Entries).FirstOrDefaultAsync();
			return phoneBook;
		}

		public async Task<PhoneBook> GetPhoneBookByNameWithEntriesAsync(string Name)
		{
			var phoneBook = await (from pb in PhoneDB.PhoneBooks where pb.Name == Name select pb).Include(pb => pb.Entries).FirstOrDefaultAsync();
			return phoneBook;
		}

		public async Task<PhoneBookEntry> CreatePhonebookEntryAsync(int PhoneBookID, string Name, string PhoneNumber)
		{
			var phoneBook = await GetPhoneBookByIDAsync(PhoneBookID);
			if (phoneBook == null)
			{
				return null;
			}
			else
			{
				try
				{
					var phoneBookEntry = new PhoneBookEntry() { Name = Name, PhoneNumber = PhoneNumber, PhoneBookId = PhoneBookID };
					PhoneDB.Add(phoneBookEntry);
					await Uow.SaveChangesAsync();
					return phoneBookEntry;
				} catch(Exception ex)
				{
					Console.WriteLine(ex.Message);
					return null;
				}
			}
		}

		public async Task<PhoneBookEntry> EditPhonebookEntryAsync(PhoneBookEntry EditedPhoneBookEntry)
		{
			var phoneBookEntry = await GetPhonebookEntryByIDAsync(EditedPhoneBookEntry.Id);
			if (phoneBookEntry == null)
			{
				return null;
			}
			else
			{
				try
				{
					phoneBookEntry.Name = EditedPhoneBookEntry.Name;
					phoneBookEntry.PhoneNumber = EditedPhoneBookEntry.PhoneNumber;
					await Uow.SaveChangesAsync();
					return phoneBookEntry;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					return null;
				}
			}
		}
		public async Task<int> DeletePhonebookEntryAsync(int PhoneBookEntryId)
		{

			var phoneBookEntry = await GetPhonebookEntryByIDAsync(PhoneBookEntryId);
			if (phoneBookEntry == null)
			{
				return 0;
			}
			else
			{
				PhoneDB.Remove(phoneBookEntry);
				return await Uow.SaveChangesAsync();
			}

		}

		public async Task<List<PhoneBookEntry>> GetEntriesForPhoneBookAsync(int PhoneBookID)
		{
			var phoneBookEntries = await (from pbe in PhoneDB.PhoneBookEntries where pbe.PhoneBookId == PhoneBookID select pbe).ToListAsync();
			return phoneBookEntries;
		}

		public async Task<PhoneBookEntry> GetPhonebookEntryByIDAsync(int EntryID)
		{
			var phoneBookEntry = await (from pbe in PhoneDB.PhoneBookEntries where pbe.Id == EntryID select pbe).FirstOrDefaultAsync();
			return phoneBookEntry;
		}

		public async Task<List<PhoneBookEntry>> FindPhonebookEntriesByNameAsync(int PhoneBookID, string NamePart)
		{
			var phoneBookEntries = await (from pbe in PhoneDB.PhoneBookEntries where pbe.PhoneBookId == PhoneBookID && pbe.Name.Contains(NamePart) select pbe).ToListAsync();
			return phoneBookEntries;
		}


	}
}
