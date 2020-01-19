using BotKeeper.Service.Core.Interfaces;
using BotKeeper.Service.Core.Interfaces.UsersInterfaces;
using BotKeeper.Service.Core.Services;
using BotKeeper.Service.Core.Services.UserServices;
using BotKeeper.Service.Core.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Core.Factories.Users {
	internal class UserServiceFactory : IUserServiceFactory {
		private IStorage storage;
		private ILogger logger;

		public UserServiceFactory(IStorage storage, ILogger logger) {
			this.storage = storage;
			this.logger = logger;
		}

		public IUserService CreateUserService(State currentState) {
			return currentState switch
			{
				SaveState _ => new SaveContextUserService(storage),
				DefaultState _ => new RegistrationContextUserService(storage, logger),
				InitState _ => new InitUserService(storage),
				GetState _ => new GetContextUserService(storage),
				_ => throw new Exception($"{nameof(currentState)}:{currentState}")
			};
		}
	}
}
