using BotKeeper.Service.Core.Interfaces;
using System.Threading.Tasks;

namespace BotKeeper.Service.Core {
    internal class BotContext {
        private State currentState;
        private readonly IStorage storage;
        private State CurrentState { 
            get => currentState; 
            set {
                Commands.CurrentState = value;
                currentState = value;
            }
        }

        public readonly ISender Sender;
        public readonly ICommandHandlerFactory HandlerFactory;
        public readonly IUserService UserService;
        public readonly IValidationService ValidationService;

		public CommandController Commands { get; }

		public BotContext(State state, IServiceFactory serviceFactory, long? userId = null) {
            Commands = new CommandController(state);
            storage = serviceFactory.Storage;

            UserService = serviceFactory.UserService;
            Sender = serviceFactory.Sender;
            HandlerFactory = serviceFactory.HandlerFactory;

            if (userId.HasValue) {
                TransitionToAsync(state, userId.Value).GetAwaiter().GetResult();
            } else {
                TransitionTo(state);
            }
        }

        public async Task TransitionToAsync(State state, long userId) {
            CurrentState = state;
            await SaveCurrentUserState(state, userId);
            CurrentState.SetContext(this);
        }

        private void TransitionTo(State state) {
            CurrentState = state;
            CurrentState.SetContext(this);
        }
        private async Task SaveCurrentUserState(State state, long userId) {
            await storage.SetUserState(userId, state);
        }
    }
}
