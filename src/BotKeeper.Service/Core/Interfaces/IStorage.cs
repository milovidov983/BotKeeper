using BotKeeper.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BotKeeper.Service.Interfaces {
	internal interface IStorage {
		Task<T> Get<T>(int userId, string key);
		Task<IEnumerable<string>> GetAllKeys(int userId);
		Task<IEnumerable<int>> GetAllUserIds();
		Task Save(int userId);
		Task Save<T>(int userId, string key, T value);
		bool IsUserExist(int id);
		Task<IPersistedUser> GetUser(int id);
	}
}