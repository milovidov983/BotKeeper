using BotKeeper.Service.Core.Factories;
using System.Threading.Tasks;
using Telegram.Bot.Args;
namespace BotKeeper.Service.Core.States {
	internal class GuestState : AbstractStateDefault {


		public override async Task DefaultAction(MessageEventArgs request) {
			context.Sender.Send("Handle Member!", request);
			await Task.Yield();
		}

		public override async Task Initial(MessageEventArgs request) {
			context.Sender.Send("Welcome member!", request);
			await Task.Yield();
		}


		public override async Task Register(MessageEventArgs request) {
			context.Sender.Send("You registered yet", request);
			await Task.Yield();
		}

		public override async Task ShowHelp(MessageEventArgs request) {
			context.Sender.Send("Member help information...", request);
			await Task.Yield();
		}

	}
}

