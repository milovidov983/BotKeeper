using BotKeeper.Service.Domain.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using BotKeeper.Service.Domain.Exceptions;

namespace BotKeeper.Service.Persistence.Db {
	public class Storage : IStorage {
		private ConcurrentDictionary<int, ConcurrentDictionary<string, string>> storage =
			new ConcurrentDictionary<int, ConcurrentDictionary<string, string>>();

		public async Task<T> Get<T>(int userId, string key) {
			await Task.Yield();
			if (storage.TryGetValue(userId, out var userStorage)) {
				var isGetStatusOk = userStorage.TryGetValue(key, out var json);
				if (isGetStatusOk) {
					var value = JsonSerializer.Deserialize<T>(json);
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
				var json = JsonSerializer.Serialize<T>(value);
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
	}
}
