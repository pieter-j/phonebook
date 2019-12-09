using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBookModels
{
	public class PhoneBook
	{
		public long ID;
		public string Name;
		public IEnumerable<PhoneBookEntry> Entries;
	}
}
