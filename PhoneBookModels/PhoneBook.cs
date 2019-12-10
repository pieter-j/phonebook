using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhoneBookModels
{
	public class PhoneBook
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<PhoneBookEntry> Entries { get; set; }

		public PhoneBook()
		{
			this.Entries = new List<PhoneBookEntry>();
		}
	}
}
