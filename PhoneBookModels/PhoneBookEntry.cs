using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhoneBookModels
{
	public class PhoneBookEntry
	{
		public int Id { get; set; }
		public int PhoneBookId { get; set; }
		public string Name { get; set; }
		public string PhoneNumber { get; set; }
	}
}
