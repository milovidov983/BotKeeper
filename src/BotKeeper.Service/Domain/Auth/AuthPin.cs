using BotKeeper.Service.Domain.Models;
using BotKeeper.Service.Domain.Store;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Domain.Auth {
	public class AuthPin {
		private readonly Dictionary<long, int> pinsCache = new Dictionary<long, int>();
		private readonly IStore store;

		public AuthPin(IStore store) {
			this.store = store;
		}

		public async Task<bool> Validate(int inputPin, IUser user) {
			var isCached = pinsCache.TryGetValue(user.Id, out var pin);

			if (!isCached) {
				pin = await store.GetPin(user);
				pinsCache.TryAdd(user.Id, pin);
			}

			if(inputPin == pin) {
				return true;
			}
			return false;
		}
	}
}
