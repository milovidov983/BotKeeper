using BotKeeper.Service.Core.Factories;
using BotKeeper.Service.Core.Helpers;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.States {
	internal class SaveTextState : AbstractStateDefault {
		public override async Task DefaultAction(MessageEventArgs request) {
			await Task.Yield();
			var key = request.Message.Text;

			if (key == string.Empty) {
				context.Sender.Send("Your key must not be empty! Shall I create the key myself?(yes/no) ", request);
			} else {
				// check uique
			}
		}

		public override async Task No(MessageEventArgs request) {
			await context.TransitionToAsync(typeof(MemberState), request.GetUserId());
			await commands.Save(request);
		}
	}
}
