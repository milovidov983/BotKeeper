using BotKeeper.Service.Core.Factories;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.States {
	internal class MemberState : State {

		public override async Task Save(MessageEventArgs request) {
			context.Sender.Send(@"Automatically assign an access key to an item?(\yes or \no ) ");

			await context.TransitionToAsync(typeof(CreateKeyState), request.Message.From.Id);
		}

		public override async Task ShowHelp(MessageEventArgs request) {
			// todo create IHelpService
			context.Sender.Send("Help VerifiedUser!");
			await Task.Yield();
		}

		public override async Task Get(MessageEventArgs request) {
			// TODO \last (number)
			context.Sender.Send(@"\last OR \keys");
			await context.TransitionToAsync(typeof(GetState), request.Message.From.Id);
		}
	}
}
