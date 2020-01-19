using BotKeeper.Service.Core.Factories;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.States {
	internal class AdminState : State {

		public override async Task DefaultAction(MessageEventArgs request) {
			context.Sender.Send("Handle Admin!");
			await Task.Yield();
		}

		public override async Task Initial(MessageEventArgs request) {
			context.Sender.Send("Welcome Admin!");
			await Task.Yield();
		}
		public override async Task ShowHelp(MessageEventArgs request) {
			context.Sender.Send("Admin help information...");
			await Task.Yield();
		}
	}
}
