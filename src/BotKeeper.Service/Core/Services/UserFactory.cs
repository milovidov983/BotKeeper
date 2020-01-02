using BotKeeper.Service.Core.Models.Users;
using BotKeeper.Service.Interfaces;
using BotKeeper.Service.Models;
using BotKeeper.Service.Models.Users;
using BotKeeper.Service.Persistence.Db;
using System;
using System.Threading.Tasks;

namespace BotKeeper.Service.Services {
	internal class UserFactory : IUserFactory {
		private readonly IStorage storage;

		public UserFactory(IStorage storage) {
			this.storage = storage ?? throw new ArgumentNullException(nameof(storage));
		}

		public async Task<IUser> Create(int id) {
			var isUserExist = storage.IsUserExist(id);
			if (isUserExist) {
				var persistedUser = await storage.GetUser(id);
				return (persistedUser?.Type) switch
				{
					"user" => new User(persistedUser),
					"admin" => new Admin(persistedUser),
					"new" => new NewUser(persistedUser),
					_ => throw new NotImplementedException($"Unknown user type {persistedUser?.Type}")
				};
			}
			return new UnknownUser(id);
		}
	}
}
