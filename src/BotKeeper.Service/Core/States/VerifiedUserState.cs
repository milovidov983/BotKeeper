using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.States {
	internal class VerifiedUserState : State {
		public override void Handle(MessageEventArgs messageEventArgs) {
			context.Sender.Send("Handle VerifiedUser!", messageEventArgs);
		}

		public override void Initial(MessageEventArgs messageEventArgs) {
			context.Sender.Send("Initial VerifiedUser!", messageEventArgs);
		}

		public override void Login(MessageEventArgs messageEventArgs) {
			context.Sender.Send("Login VerifiedUser!", messageEventArgs);
		}

		public override void Register(MessageEventArgs messageEventArgs) {
		}

		public override void ShowHelp(MessageEventArgs messageEventArgs) {
			context.Sender.Send("Help VerifiedUser!", messageEventArgs);
		}
	}
}
