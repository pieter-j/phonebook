using PhoneBookInterfaces;
using System.Threading.Tasks;

namespace PhoneBookData.Repo
{

	public class UnitOfWork: IUnitofWork
	{
		protected PhoneContext PhoneDB;

		public UnitOfWork(PhoneContext db)
		{
			PhoneDB = db;
		}

		public void SaveChanges()
		{
			PhoneDB.SaveChanges();
		}

		public async Task<int> SaveChangesAsync()
		{
			return await PhoneDB.SaveChangesAsync();
		}
	}
}
