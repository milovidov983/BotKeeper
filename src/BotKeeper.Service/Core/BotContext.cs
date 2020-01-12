using BotKeeper.Service.Core.Helpers;
using BotKeeper.Service.Core.Interfaces;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core {
    internal class BotContext {
        private State currentState;
        private readonly IStorage storage;

        public readonly ISender Sender;
        public readonly IStratagyRepository HandlerFactory;
        public readonly IUserService UserService;
        public readonly IValidationService ValidationService;

        public BotContext(State state, IServiceFactory serviceFactory, long? userId = null) {
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
            currentState = state;
            await SaveCurrentUserState(state, userId);
            currentState.SetContext(this);
        }

        private void TransitionTo(State state) {
            currentState = state;
            currentState.SetContext(this);
        }
        private async Task SaveCurrentUserState(State state, long userId) {
            await storage.SetUserState(userId, state);
        }

        #region Commands

        public async Task Handle(MessageEventArgs request) {
            await currentState.Handle(request);
        }

        [Command(@"\init")]
        public async Task InitialState(MessageEventArgs request) {
            await currentState.Initial(request);
        }

        [Command(@"\help")]
        public async Task ShowHelp(MessageEventArgs request) {
            await currentState.ShowHelp(request);
        }

        [Command(@"\register")]
        public async Task Register(MessageEventArgs request) {
            await currentState.Register(request);
        }
        

        [Command(@"\save")]
        public async Task Save(MessageEventArgs request) {
            await currentState.Save(request);
        }

        #endregion
    }
}
