using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.States {
	internal class MemberState : State {
		public override async Task Handle(MessageEventArgs request) {
			context.Sender.Send("Handle VerifiedUser!", request);
			await Task.Yield();
		}

		public override async Task Initial(MessageEventArgs request) {
			context.Sender.Send("Initial VerifiedUser!", request);
			await Task.Yield();
		}

		public override async Task Login(MessageEventArgs request) {
			context.Sender.Send("Login VerifiedUser!", request);
			await Task.Yield();
		}

		public override Task No(MessageEventArgs request) {
			throw new NotImplementedException();
		}

		public override async Task Register(MessageEventArgs request) {
			await Task.Yield();
		}

		public override async Task Save(MessageEventArgs request) {
			context.Sender.Send("Enter key:", request);
			await context.TransitionToAsync(new SaveKeyState(), request.Message.From.Id);
		}

		public override async Task ShowHelp(MessageEventArgs request) {
			context.Sender.Send("Help VerifiedUser!", request);
			await Task.Yield();
		}

		public override Task Yes(MessageEventArgs request) {
			throw new NotImplementedException();
		}
	}
}
