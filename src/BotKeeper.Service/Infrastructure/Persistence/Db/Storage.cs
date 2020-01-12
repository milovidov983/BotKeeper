using BotKeeper.Service.Core;
using BotKeeper.Service.Core.Interfaces;
using BotKeeper.Service.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BotKeeper.Service.Persistence.Db {
	internal class StorageResult<T> : IStorageResult<T> {
		private T result;
		public bool HasResult { get; set; }
		public T Result { 
			get {
				return result;
			}
			set {
				result = value;
				HasResult = true;
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
		private ConcurrentDictionary<long, PersistedUser> users = new ConcurrentDictionary<long, PersistedUser>();

		/// <summary>
		/// Users sates
		/// </summary>
		private ConcurrentDictionary<long, string> userStates = new ConcurrentDictionary<long, string>();

		public Storage() {
			LoadDb();
		}

		#region Data access methods
		public async Task<IStorageResult<string>> GetUserState(long id) {
			await Task.Yield();
			var hasCache = userStates.TryGetValue(id, out var cachedState);
			if (hasCache) {
				return new StorageResult<string> { Result = cachedState };
			}
			return new StorageResult<string>();
		}
		public async Task SetUserState(long id, State state) {
			await Task.Yield();
			userStates.AddOrUpdate(id, state.GetType().Name, (_, oldState) => state.GetType().Name);
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
				throw new BotException("User not found", StatusCodes.NotFound);
			}
		}

		public async Task Save(long userId) {
			await Task.Yield();
			var isUserAdded = await CreateNewUser(userId);

			if (!isUserAdded) {
				throw new BotException($"User {userId} already exists", StatusCodes.InvalidRequest);
			}

			var isUserStorageCreated = await CreateUserStorage(userId);
			if (!isUserStorageCreated) {
				throw new BotException($"User storage {userId} already exists", StatusCodes.InvalidRequest);
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
				throw new BotException("User not found", StatusCodes.NotFound);
			}
		}

		public async Task<bool> IsUserExist(long id) {
			await Task.Yield();
			return users.ContainsKey(id);
		}

		public async Task<IStorageResult<PersistedUser>> GetUser(long id) {
			await Task.Yield();
			if (users.TryGetValue(id, out var persistedUser)) {
				return new StorageResult<PersistedUser> { Result = persistedUser };
			}
			return new StorageResult<PersistedUser>();
		}

		#endregion

		#region Helpers
		private async Task<bool> CreateNewUser(long id) {
			await Task.Yield();
			var newUser = new PersistedUser {
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
			var userDbJson = LoadFromFile(fileDb);
			var usersJson = LoadFromFile(fileUsers);
			var userStatesJson = LoadFromFile(fileUserStates);

			storage = JsonConvert.DeserializeObject<ConcurrentDictionary<long, ConcurrentDictionary<string, string>>>(userDbJson)
				?? new ConcurrentDictionary<long, ConcurrentDictionary<string, string>>();

			users = JsonConvert.DeserializeObject<ConcurrentDictionary<long, PersistedUser>>(usersJson)
				?? new ConcurrentDictionary<long, PersistedUser>();

			userStates = JsonConvert.DeserializeObject<ConcurrentDictionary<long, string>>(userStatesJson)
				?? new ConcurrentDictionary<long, string>();
		}

		private string LoadFromFile(string path) {
			if (File.Exists(path)) {
				return File.ReadAllText(path);
			}

			File.CreateText(path).Close();
			return string.Empty;
		}

		#endregion

		public void Dispose() {
			SaveDbToFileSystem().GetAwaiter().GetResult();
		}
	}
}
