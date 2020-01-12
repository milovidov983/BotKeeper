using BotKeeper.Service.Core.Interfaces;
using BotKeeper.Service.Core.States;
using System;
using System.Threading.Tasks;

namespace BotKeeper.Service.Core.Factories {

	internal class ContextFactory : IContextFactory {
		private readonly IStorage storage;
		private readonly IServiceFactory serviceFactory;
		private readonly IUserService userService;
		private readonly IStateFactory stateFactory;

		public ContextFactory(IStorage storage, IUserService userService, IStateFactory stateFactory, IServiceFactory serviceFactory) {
			this.storage = storage ?? throw new ArgumentNullException(nameof(storage));
			this.serviceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
			this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
			this.stateFactory = stateFactory ?? throw new ArgumentNullException(nameof(stateFactory));
		}

		public async Task<BotContext> CreateContext(long userId) {
			var storedUserState = await storage.GetUserState(userId);

			if (storedUserState.HasResult) {
				var storedState = stateFactory.Create(storedUserState.Result, $"user {userId}");
				return new BotContext(storedState, serviceFactory);
			} else {
				var isUserExist = await userService.IsUserExist(userId);
				if (isUserExist) {
					var guestState = stateFactory.Create(typeof(GuestState));
					return new BotContext(guestState, serviceFactory, userId);
				}
			}

			return new BotContext(stateFactory.DefaultState, serviceFactory);
		}
	}
}
