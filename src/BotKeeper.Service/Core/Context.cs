using BotKeeper.Service.Core.Helpers;
using BotKeeper.Service.Core.Interfaces;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core {
    internal class Context {
        private State currentState = null;
        private readonly IStorage storage;

        public readonly ISender Sender;
        public readonly IHandlerFactory HandlerFactory;
        public readonly IUserService UserService;
        public readonly IValidationService ValidationService;

        public Context(State state, IServiceFactory serviceFactory, long? userId = null) {
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

        private void TransitionTo(State state) {
            currentState = state;
            currentState.SetContext(this);
        }


        public async Task TransitionToAsync(State state, long userId) {
            currentState = state;
            await storage.SetUserState(userId, state);
            currentState.SetContext(this);
        }

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
        
        public async Task Handle(MessageEventArgs request) {
            await currentState.Handle(request);
        }

        [Command(@"\login")]
        public async Task Login(MessageEventArgs request) {
            await currentState.Login(request);
        }

        public async Task Save(MessageEventArgs request) {
            await currentState.Save(request);
        }
    }
}
