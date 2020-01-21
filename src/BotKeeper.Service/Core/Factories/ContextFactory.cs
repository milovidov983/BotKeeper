using BotKeeper.Service.Core.Helpers;
using BotKeeper.Service.Core.Interfaces;
using BotKeeper.Service.Core.Interfaces.UsersInterfaces;
using BotKeeper.Service.Core.States;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Factories {


	internal class ContextFactory : IContextFactory {
		private readonly IStorage storage;
		private readonly IServiceFactory serviceFactory;
		private readonly IInitUserService userService;
		private readonly IStateFactory stateFactory;

		public ContextFactory(IStorage storage, IUserServiceFactory userServiceFactory, IStateFactory stateFactory, IServiceFactory serviceFactory) {
			this.storage = storage ?? throw new ArgumentNullException(nameof(storage));
			this.serviceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
			this.stateFactory = stateFactory ?? throw new ArgumentNullException(nameof(stateFactory));
			this.userService = (IInitUserService)userServiceFactory.CreateUserService(new InitState());
		}

		/// <summary>
		/// Try restore the previous state of the user
		/// If the state is not saved, create the state in accordance with the rules.
		/// </summary>
		public async Task<BotContext> CreateContext(MessageEventArgs request) {
			(State state, BotContext context) current;
			var userId = request.GetUserId();
			var storedUserState = await userService.GetUserState(userId);

			if (storedUserState != null) {
				current = CreateStoredStateContext(request, userId, storedUserState);
			} else {
				current = await CreateMemberContext(request, userId);
			}

			if (current.IsNotInit()) {
				current = CreateDefaultContext(request);
			}

			await current.context.MakeTransitionTo(current.state);
			return current.context;
		}

		#region Helpers
		private (State state, BotContext context) CreateDefaultContext(MessageEventArgs request) {
			(State state, BotContext context) current = (null, null);
			current.state = stateFactory.DefaultState;
			current.context = new BotContext(current.state, serviceFactory, request);
			return current;
		}



		private async Task<(State state, BotContext context)> CreateMemberContext(
			MessageEventArgs request,
			long userId) {

			(State state, BotContext context) contextState = (null, null);
			var isUserExist = await userService.IsUserExist(userId);
			if (isUserExist) {
				contextState.state = stateFactory.Create(typeof(MemberState));
				contextState.context = new BotContext(contextState.state, serviceFactory, request, userId);
			}
			return contextState;
		}

		private (State state, BotContext context) CreateStoredStateContext(
			MessageEventArgs request,
			long userId, string
			storedUserState) {

			(State state, BotContext context) contextState;
			contextState.state = stateFactory.Create(storedUserState, $"user {userId}");
			contextState.context = new BotContext(contextState.state, serviceFactory, request, userId);
			return contextState;
		}
		#endregion
	}

	internal static class ContextFactoryExt {
		public static bool IsNotInit(this (State state, BotContext context) current) {
			return current.state is null;
		}

	}
}