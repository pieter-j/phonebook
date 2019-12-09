using System.Threading.Tasks;

namespace PhoneBookInterfaces
{
	public interface IUnitofWork
	{
		void SaveChanges();
		Task<int> SaveChangesAsync();
	}
}
