using BotKeeper.Service.Core.interfaces;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core {
    internal class Context {
        private State currentState = null;
        private readonly IStorage storage;

        public readonly ISender Sender;
        public readonly IParserService ParserService;
        public readonly IUserService UserService;
        public readonly IValidationService ValidationService;

        public Context(State state, IServiceFactory serviceFactory) {
            TransitionTo(state);
            storage = serviceFactory.Storage;
            UserService = serviceFactory.UserService;
            Sender = serviceFactory.Sender;
            ParserService = serviceFactory.ParserService;
        }

        private void TransitionTo(State state) {
            currentState = state;
            currentState.SetContext(this);
        }


        public async Task TransitionToAsync(State state, long? userId = null) {
            if(currentState == state) {
                return;
            }
            currentState = state;
            if (userId.HasValue) {
                await storage.SetUserState(userId.Value, state);
            }
            currentState.SetContext(this);
        }

        public async Task InitialState(MessageEventArgs request) {
            await currentState.Initial(request);
        }

        public async Task ShowHelp(MessageEventArgs request) {
            await currentState.ShowHelp(request);
        }

        public async Task Register(MessageEventArgs request) {
            await currentState.Register(request);
        }       
        public async Task Handle(MessageEventArgs request) {
            await currentState.Handle(request);
        }

        public async Task Login(MessageEventArgs request) {
            await currentState.Login(request);
        }
    }
}
