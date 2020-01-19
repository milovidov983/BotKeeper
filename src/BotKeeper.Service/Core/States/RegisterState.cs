using BotKeeper.Service.Core.Factories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.States {
	internal class RegisterState : State {
		private Dictionary<long, int> count = new Dictionary<long, int>();

		public override async Task DefaultAction(MessageEventArgs request) {
			await Register(request);
		}

		public override async Task ShowHelp(MessageEventArgs request) {
			context.Sender.Send("Register help information", request);
			await Task.Yield();
		}
	}
}
