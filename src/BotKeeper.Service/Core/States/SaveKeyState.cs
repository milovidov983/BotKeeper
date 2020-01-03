using BotKeeper.Service.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.States {
	internal class SaveKeyState : State {
		public override async Task Handle(MessageEventArgs request) {
			var key = request.Message.Text?.Trim()?.ToLowerInvariant();
			if (string.IsNullOrEmpty(key)) {
				context.Sender.Send("Your key must not be empty! Shall I create the key myself?(yes/no) ", request);
			} else {
				// check uique
			}
		}

		public override Task Initial(MessageEventArgs request) {
			throw new NotImplementedException();
		}

		public override Task Login(MessageEventArgs request) {
			throw new NotImplementedException();
		}

		public override async Task No(MessageEventArgs request) {
			await context.TransitionToAsync(new MemberState(), request.GetUserId());
			await context.Save(request);
		}

		public override Task Register(MessageEventArgs request) {
			throw new NotImplementedException();
		}

		public override Task Save(MessageEventArgs request) {
			throw new NotImplementedException();
		}

		public override Task ShowHelp(MessageEventArgs request) {
			throw new NotImplementedException();
		}

		public override Task Yes(MessageEventArgs request) {
			throw new NotImplementedException();
		}
	}
}
