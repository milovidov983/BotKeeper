using BotKeeper.Service.Domain.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
namespace BotKeeper.Service.Persistence.Db {
	public class Storage : IStorage {
		private ConcurrentDictionary<int, ConcurrentDictionary<string, string>> storage = 
			new ConcurrentDictionary<int, ConcurrentDictionary<string, string>>();
		public async Task Save<T>(int userId, string key, T value) {
			await Task.Yield();
			if (storage.TryGetValue(userId, out var userStorage)) {
				var json = JsonSerializer.Serialize<T>(value);
				var saveStatusOk = userStorage.TryAdd(key, json);
				if(saveStatusOk) {
					return;
				} else {
					userStorage[key] = json;
				}
			}
		}
	}
}
