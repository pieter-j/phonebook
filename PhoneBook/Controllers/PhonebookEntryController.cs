using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneBookInterfaces;
using PhoneBookModels;

namespace PhoneBook.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PhonebookEntryController : ControllerBase
	{
		IPhoneBookService PhonebookS;

		public PhonebookEntryController(IPhoneBookService PhonebookService)
		{
			PhonebookS = PhonebookService;
		}

		// GET: api/PhonebookEntry/forPhone/1
		[HttpGet("forPhone/{phoneId:int}", Name = "GetPhoneBookEntries")]
		public async Task<List<PhoneBookEntry>> GetForPhoneAsync(int phoneId)
		{
			return await PhonebookS.GetPhoneBookEntriesAsync(phoneId);
		}
		
		// GET: api/PhonebookEntry/forPhone/1
		[HttpGet("{phoneId:int}/{search}", Name = "SearchPhoneBookEntries")]
		public async Task<List<PhoneBookEntry>> GetForPhoneAsync(int phoneId, string search)
		{
			return await PhonebookS.FindPhonebookEntriesByNameAsync(phoneId, search);
		}

		// POST: api/PhonebookEntry
		[HttpPost]
		public async Task<ActionResult<PhoneBookEntry>> PostAsync([FromBody] PhoneBookEntry PhonebookEntry)
		{
			(PhoneBookEntry phonebookEntry, List<ValidationError> errors) = await PhonebookS.CreatePhonebookEntryAsync(PhonebookEntry.PhoneBookId, PhonebookEntry.Name, PhonebookEntry.PhoneNumber);
			if (errors == null  || errors.Count == 0)
			{
				return phonebookEntry;
			}
			else
			{
				return BadRequest(errors);
			}

		}

		//PUT: api/PhonebookEntry/5
		[HttpPut("{id}")]
		public async Task<ActionResult<PhoneBookEntry>> PutAsync(int id, [FromBody] PhoneBookEntry PhonebookEntry)
		{
			(PhoneBookEntry phonebookEntry, List<ValidationError> errors) = await PhonebookS.EditPhonebookEntryAsync(PhonebookEntry);
			if (errors == null || errors.Count == 0)
			{
				return phonebookEntry;
			}
			else
			{
				return BadRequest(errors);
			}
		}

		// DELETE: api/ApiWithActions/5
		[HttpDelete("{id}")]
		public async void Delete(int id)
		{
			await PhonebookS.DeletePhonebookEntryAsync(id);
		}
	}
}
