using BotKeeper.Service.Core.interfaces;
using BotKeeper.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BotKeeper.Service.Core {
	internal interface IStorage {
		Task<T> Get<T>(long userId, string key);
		Task<IEnumerable<string>> GetAllKeys(long userId);
		Task<IEnumerable<long>> GetAllUserIds();
		Task Save(long userId);
		Task Save<T>(long userId, string key, T value);
		bool IsUserExist(long id);
		Task<IPersistedUser> GetUser(long id);
		Task<IInteraction> GetSession(long userId);
		void SetUserState(long id, State state);
		IStorageResult<State> GetUserState(long id);
	}
}