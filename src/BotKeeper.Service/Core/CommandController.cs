using BotKeeper.Service.Core.Helpers;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core {
    internal class CommandController {
        public State CurrentState;

        public CommandController(State state) {
            CurrentState = state;
        }

        public async Task DefaultAction(MessageEventArgs request) {
            await CurrentState.DefaultAction(request);
        }

        [Command(@"\init")]
        public async Task InitialState(MessageEventArgs request) {
            await CurrentState.Initial(request);
        }

        [Command(@"\help")]
        public async Task ShowHelp(MessageEventArgs request) {
            await CurrentState.ShowHelp(request);
        }

        [Command(@"\register")]
        public async Task Register(MessageEventArgs request) {
            await CurrentState.Register(request);
        }

        [Command(@"\save")]
        public async Task Save(MessageEventArgs request) {
            await CurrentState.Save(request);
        }
    }
}