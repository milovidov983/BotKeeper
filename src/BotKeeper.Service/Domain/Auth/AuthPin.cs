using BotKeeper.Service.Domain.Models;
using BotKeeper.Service.Domain.Store;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Domain.Auth {
	public class AuthPin {
		private readonly IStore store;

		public AuthPin(IStore store) {
			this.store = store;
		}

		public int MAX_PIN_ATTEMPTS { get; private set; }

		public async Task<bool> Validate(int inputPin, IUser user) {
			await CheckArguments(inputPin, user);
			await CheckBusinessRules(user);

			var pin = await store.GetPin(user);
			if (inputPin == pin) {
				await user.ResetPinAttempt();
				return true;
			}
			await user.AddAttempt();
			return false;
		}

		private async Task CheckBusinessRules(IUser user) {
			int attempts = await user.GetPinAttempts();
			if (attempts >= MAX_PIN_ATTEMPTS) {
				await user.LockPin();
				throw new SecurityException($"Maximum number of pin attempts was reached.");
			}
		}

		private async Task CheckArguments(int inputPin, IUser user) {
			if (inputPin == default) {
				throw new ArgumentException($"{nameof(inputPin)} can not be default value.");
			}
			if (user is null) {
				throw new ArgumentNullException($"{nameof(user)} can not be null.");
			}

		}
	}
}
