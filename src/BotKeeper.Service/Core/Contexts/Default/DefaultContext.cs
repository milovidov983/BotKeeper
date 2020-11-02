using BotKeeper.Service.Core.Contexts;
using BotKeeper.Service.Core.Helpers;
using BotKeeper.Service.Core.Models;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core {

	internal class DefaultContext : BaseContext {

		public AbstractStateDefault CurrentState { get; set; }


		public DefaultContext(AbstractStateDefault state) {
			CurrentState = state;
		}

		public async Task DefaultAction(MessageEventArgs request) {
			await CurrentState.DefaultAction(request);
		}

		[Command(AllCommands.Help)]
		public async Task ShowHelp(MessageEventArgs request) {
			await CurrentState.ShowHelp(request);
		}

		[Command(AllCommands.Register)]
		public async Task Register(MessageEventArgs request) {
			await CurrentState.Register(request);
		}

		[Command(AllCommands.Save)]
		public async Task Save(MessageEventArgs request) {
			await CurrentState.Save(request);
		}

		[Command(AllCommands.Yes)]
		public async Task Yes(MessageEventArgs request) {
			await CurrentState.Yes(request);
		}

		[Command(AllCommands.No)]
		public async Task No(MessageEventArgs request) {
			await CurrentState.No(request);
		}
		[Command(AllCommands.Get)]
		public async Task Get(MessageEventArgs request) {
			await CurrentState.Get(request);
		}
	}
}