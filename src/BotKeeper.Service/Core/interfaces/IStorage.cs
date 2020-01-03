using BotKeeper.Service.Core.interfaces;
using BotKeeper.Service.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BotKeeper.Service.Core.interfaces {
	internal interface IStorage {
		Task<IStorageResult<T>> Get<T>(long userId, string key);
		Task<IEnumerable<string>> GetAllKeys(long userId);
		Task<IEnumerable<long>> GetAllUserIds();
		Task Save(long userId);
		Task Save<T>(long userId, string key, T value);
		Task<bool> IsUserExist(long id);
		Task<IStorageResult<IPersistedUser>> GetUser(long id);
		Task SetUserState(long id, State state);
		Task<IStorageResult<State>> GetUserState(long id);
	}
}