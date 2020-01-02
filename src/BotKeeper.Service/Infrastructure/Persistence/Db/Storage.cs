using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using BotKeeper.Service.Models;
using BotKeeper.Service.Interfaces;
using BotKeeper.Service.Core.Models.Users;
using BotKeeper.Service.Core.Models;
using System.Linq;

namespace BotKeeper.Service.Persistence.Db {
	internal class Storage : IStorage, IDisposable {
		private ConcurrentDictionary<int, ConcurrentDictionary<string, string>> storage =
			new ConcurrentDictionary<int, ConcurrentDictionary<string, string>>();

		private ConcurrentDictionary<int, IPersistedUser> users = new ConcurrentDictionary<int, IPersistedUser>();
		private HashSet<int> userCachedIds = new HashSet<int>();

		public async Task<T> Get<T>(int userId, string key) {
			await Task.Yield();
			if (storage.TryGetValue(userId, out var userStorage)) {
				var isGetStatusOk = userStorage.TryGetValue(key, out var json);
				if (isGetStatusOk) {
					var value = JsonConvert.DeserializeObject<T>(json);
					return value;
				} else {
					throw new StorageException("Key not found");
				}
			} else {
				throw new StorageException("User not found");
			}
		}

		public async Task Save<T>(int userId, string key, T value) {
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

		public async Task Save(int userId) {
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

		public async Task<IEnumerable<int>> GetAllUserIds() {
			await Task.Yield();
			return storage.Keys;
		}

		public async Task<IEnumerable<string>> GetAllKeys(int userId) {
			await Task.Yield();
			if (storage.TryGetValue(userId, out var userStorage)) {
				return userStorage.Keys;
			} else {
				throw new StorageException("User not found");
			}
		}

		public bool IsUserExist(int id) {
			return userCachedIds.Contains(id);
		}

		public async Task<IPersistedUser> GetUser(int id) {
			await Task.Yield();
			if (users.TryGetValue(id, out var persistedUser)) {
				return persistedUser;
			} else {
				throw new StorageException("User not found");
			}
		}


		#region Helpers
		private async Task<bool> CreateNewUser(int id) {
			await Task.Yield();
			var newUser = new PersistedUser {
				Id = id,
				Data = string.Empty,
				Type = UserTypes.New
			};
			if (users.TryAdd(id, newUser)) {
				userCachedIds.Add(id);
				return true;
			}
			return false;
		}

		private async Task<bool> CreateUserStorage(int userId) {
			await Task.Yield();
			return storage.TryAdd(userId, new ConcurrentDictionary<string, string>());
		}
		#endregion

		#region File system

		private readonly string fileDb = @"db.json";
		private readonly string fileUsers = @"users.json";
		public async Task SaveDbToFileSystem() {
			var jsonDb = JsonConvert.SerializeObject(storage);
			var jsonUsers = JsonConvert.SerializeObject(users);

			using StreamWriter dbSw = File.CreateText(fileDb);
			using StreamWriter usersSw = File.CreateText(fileUsers);

			await Task.WhenAll(
				dbSw.WriteLineAsync(jsonDb),
				usersSw.WriteLineAsync(jsonUsers)
			);
		}

		public void LoadDb() {
			var data = File.ReadAllText(fileDb);
			var usersDb = File.ReadAllText(fileUsers);

			storage = JsonConvert.DeserializeObject<ConcurrentDictionary<int, ConcurrentDictionary<string, string>>>(data);
			users = JsonConvert.DeserializeObject<ConcurrentDictionary<int, IPersistedUser>>(usersDb);
			userCachedIds = users.Keys.Select(id => id).ToHashSet();
		}

		#endregion


		public void Dispose() {
			SaveDbToFileSystem().GetAwaiter().GetResult();
		}
	}
}
