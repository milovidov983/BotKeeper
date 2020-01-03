using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using BotKeeper.Service.Core;
using BotKeeper.Service.Core.interfaces;
using BotKeeper.Service.Core.Models;

namespace BotKeeper.Service.Persistence.Db {


	internal class StorageResult<T> : IStorageResult<T> {
		private T result;
		private bool hasResult;
		public bool HasResult { get { return hasResult; } set { hasResult = value; } }
		public T Result { 
			get {
				return result;
			}
			set {
				result = value;
				hasResult = true;
			} 
		}
	}

	internal class Storage : IStorage, IDisposable {
		/// <summary>
		/// Users stores
		/// </summary>
		private ConcurrentDictionary<long, ConcurrentDictionary<string, string>> storage =
			new ConcurrentDictionary<long, ConcurrentDictionary<string, string>>();

		/// <summary>
		/// Users db
		/// </summary>
		private ConcurrentDictionary<long, IPersistedUser> users = new ConcurrentDictionary<long, IPersistedUser>();

		/// <summary>
		/// Users sates
		/// </summary>
		private ConcurrentDictionary<long, State> userStates = new ConcurrentDictionary<long, State>();


		#region Data access methods
		public async Task<IStorageResult<State>> GetUserState(long id) {
			await Task.Yield();
			var hasCache = userStates.TryGetValue(id, out var cachedState);
			if (hasCache) {
				return new StorageResult<State> { Result = cachedState };
			}
			return new StorageResult<State>();
		}
		public async Task SetUserState(long id, State state) {
			await Task.Yield();
			userStates.AddOrUpdate(id, state, (_, oldState) => state);
		}

		public async Task<IStorageResult<T>> Get<T>(long userId, string key) {
			await Task.Yield();
			if (storage.TryGetValue(userId, out var userStorage)) {
				var isGetStatusOk = userStorage.TryGetValue(key, out var json);
				if (isGetStatusOk) {
					var value = JsonConvert.DeserializeObject<T>(json);
					return new StorageResult<T> { Result = value };
				}
			}
			return new StorageResult<T>();
		}

		public async Task Save<T>(long userId, string key, T value) {
			await Task.Yield();
			if (storage.TryGetValue(userId, out var userStorage)) {
				var json = JsonConvert.SerializeObject(value);
				var isSaveStatusOk = userStorage.TryAdd(key, json);
				if (isSaveStatusOk) {
					return;
				} else {
					userStorage[key] = json;
				}
			} else {
				throw new StorageException("User not found");
			}
		}

		public async Task Save(long userId) {
			await Task.Yield();
			var isUserAdded = await CreateNewUser(userId);

			if (!isUserAdded) {
				throw new StorageException($"User {userId} already exists");
			}

			var isUserStorageCreated = await CreateUserStorage(userId);
			if (!isUserStorageCreated) {
				throw new StorageException($"User storage {userId} already exists");
			}
			
		}

		public async Task<IEnumerable<long>> GetAllUserIds() {
			await Task.Yield();
			return storage.Keys;
		}

		public async Task<IEnumerable<string>> GetAllKeys(long userId) {
			await Task.Yield();
			if (storage.TryGetValue(userId, out var userStorage)) {
				return userStorage.Keys;
			} else {
				throw new StorageException("User not found");
			}
		}

		public async Task<bool> IsUserExist(long id) {
			await Task.Yield();
			return users.ContainsKey(id);
		}

		public async Task<IStorageResult<IPersistedUser>> GetUser(long id) {
			await Task.Yield();
			if (users.TryGetValue(id, out var persistedUser)) {
				return new StorageResult<IPersistedUser> { Result = persistedUser };
			}
			return new StorageResult<IPersistedUser>();
		}

		#endregion

		#region Helpers
		private async Task<bool> CreateNewUser(long id) {
			await Task.Yield();
			var newUser = new IPersistedUser {
				Id = (int)id,
				Type = UserType.Guest
			};
			return users.TryAdd(id, newUser);
		}

		private async Task<bool> CreateUserStorage(long userId) {
			await Task.Yield();
			return storage.TryAdd(userId, new ConcurrentDictionary<string, string>());
		}
		#endregion

		#region File system

		private readonly string fileDb = @"db.json";
		private readonly string fileUsers = @"users.json";
		private readonly string fileUserStates = @"userStates.json";
		public async Task SaveDbToFileSystem() {
			var dbJson = JsonConvert.SerializeObject(storage);
			var usersJson = JsonConvert.SerializeObject(users);
			var userStatesJson = JsonConvert.SerializeObject(userStates);

			using StreamWriter dbSw = File.CreateText(fileDb);
			using StreamWriter usersSw = File.CreateText(fileUsers);
			using StreamWriter userStatesSw = File.CreateText(fileUserStates);

			await Task.WhenAll(
				dbSw.WriteLineAsync(dbJson),
				usersSw.WriteLineAsync(usersJson),
				userStatesSw.WriteLineAsync(userStatesJson)
			);
		}

		public void LoadDb() {
			var userDbJson = File.ReadAllText(fileDb);
			var usersJson = File.ReadAllText(fileUsers);
			var userStatesJson = File.ReadAllText(fileUserStates);

			storage = JsonConvert.DeserializeObject<ConcurrentDictionary<long, ConcurrentDictionary<string, string>>>(userDbJson);
			users = JsonConvert.DeserializeObject<ConcurrentDictionary<long, IPersistedUser>>(usersJson);
			userStates = JsonConvert.DeserializeObject<ConcurrentDictionary<long, State>>(userStatesJson);
		}

		#endregion

		public void Dispose() {
			SaveDbToFileSystem().GetAwaiter().GetResult();
		}

	}
}
