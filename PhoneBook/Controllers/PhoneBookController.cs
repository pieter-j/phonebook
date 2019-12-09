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
	public class PhonebookController : ControllerBase
	{
		IPhoneBookService PhonebookS;

		public PhonebookController(IPhoneBookService PhonebookService)
		{
			PhonebookS = PhonebookService;
		}

		// GET: api/PhoneBook/Pieter
		[HttpGet("{Name}", Name = "GetPhoneBook")]
		public async Task<ActionResult<PhoneBookModels.PhoneBook>> GetAsync(string Name)
		{
			var pb = await PhonebookS.GetPhoneBookAsync(Name);
			if (pb != null)
			{
				return pb;
			} else
			{
				(PhoneBookModels.PhoneBook phoneBook, List<ValidationError> errors) = await PhonebookS.CreatePhoneBookAsync(Name);
				if (errors.Count == 0)
				{
					return phoneBook;
				}
				else
				{
					return BadRequest(errors);
				}
			}

		}

		// POST: api/PhoneBook
		[HttpPost]
		public async Task<ActionResult<PhoneBookModels.PhoneBook>> PostAsync([FromBody] string Name)
		{
			(PhoneBookModels.PhoneBook phoneBook, List<ValidationError> errors) =  await PhonebookS.CreatePhoneBookAsync(Name);
			if(errors.Count == 0)
			{
				return phoneBook;
			} else
			{
				return BadRequest(errors);
			}

		}
	}
}
