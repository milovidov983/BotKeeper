using BotKeeper.Service.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BotKeeper.Service.Core.Interfaces {
	internal interface IStorage {
		Task<IStorageResult<T>> Get<T>(long userId, string key);
		Task<IEnumerable<string>> GetAllKeys(long userId);
		Task<IEnumerable<long>> GetAllUserIds();
		Task CreateNewUser(long userId);
		Task Save<T>(long userId, string key, T value);
		Task<bool> IsUserExist(long id);
		Task<IStorageResult<PersistedUser>> GetUser(long id);

		Task SetUserState(long id, State state);
		Task<IStorageResult<string>> GetUserState(long id);
		Task SetUserKey(long id, string key);
		Task SetLastKey(long id, string key);
	}
}