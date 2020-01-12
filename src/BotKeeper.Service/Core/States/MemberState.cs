using BotKeeper.Service.Core.Factories;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.States {
	internal class MemberState : State {
		public MemberState(IStateFactory stateFactory) : base(stateFactory) {
		}

		public override async Task Save(MessageEventArgs request) {
			context.Sender.Send("Enter key:", request);
			var saveKeyState = stateFactory.Create(typeof(SaveKeyState));
			await context.TransitionToAsync(saveKeyState, request.Message.From.Id);
		}

		public override async Task ShowHelp(MessageEventArgs request) {
			// todo create IHelpService
			context.Sender.Send("Help VerifiedUser!", request);
			await Task.Yield();
		}
	}
}
