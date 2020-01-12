using BotKeeper.Service.Core.Helpers;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.States {
	internal class SaveKeyState : State {
		public override async Task Handle(MessageEventArgs request) {
			var key = request.Message.Text?.Trim()?.ToLowerInvariant();

			if (IsUserKeyNotSet(key)) {
				context.Sender.Send("Your key must not be empty! Shall I create the key myself?(yes/no) ", request);
			} else {
				// check uique
			}
		}

		private static bool IsUserKeyNotSet(string key) {
			return string.IsNullOrEmpty(key);
		}

		public override async Task No(MessageEventArgs request) {
			await context.TransitionToAsync(new MemberState(), request.GetUserId());
			await context.Save(request);
		}
	}
}
