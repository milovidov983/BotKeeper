using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using BotKeeper.Service.Models;
using BotKeeper.Service.Interfaces;

namespace BotKeeper.Service.Persistence.Db {
	internal class Storage : IStorage, IDisposable {
		private ConcurrentDictionary<int, ConcurrentDictionary<string, string>> storage =
			new ConcurrentDictionary<int, ConcurrentDictionary<string, string>>();

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
			var isUserAdded = storage.TryAdd(userId, new ConcurrentDictionary<string, string>());
			if (isUserAdded) {
				return;
			} else {
				throw new StorageException("User already exists");
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


		public void Save() {
			var db = JsonConvert.SerializeObject(storage);
			string path = @"db.json";

				// Create a file to write to.
			using (StreamWriter sw = File.CreateText(path)) {
				sw.WriteLine(db);
			}
			
		}
		public void Load() {
			string path = @"db.json";
			var data = File.ReadAllText(path);
			storage = JsonConvert.DeserializeObject<ConcurrentDictionary<int, ConcurrentDictionary<string, string>>>(data);
		}

		public void Dispose() {
			Save();
		}


		private readonly HashSet<int> userCachedIds = new HashSet<int>();
		public Task<IUser> GetUser(int id) {
			if (userCachedIds.Contains(id)) {
				// get user from db
			}

			return null;
		}
	}
}
