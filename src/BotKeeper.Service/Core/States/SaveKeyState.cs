using BotKeeper.Service.Core.Factories;
using BotKeeper.Service.Core.Helpers;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.States {
	internal class SaveKeyState : State {
		public SaveKeyState(IStateFactory stateFactory) : base(stateFactory) {
		}

		public override async Task Handle(MessageEventArgs request) {
			await Task.Yield();
			var key = request.ClearTextMessage();

			if (key == string.Empty) {
				context.Sender.Send("Your key must not be empty! Shall I create the key myself?(yes/no) ", request);
			} else {
				// check uique
			}
		}

		public override async Task No(MessageEventArgs request) {
			var memberState = stateFactory.GetState(typeof(MemberState));
			await context.TransitionToAsync(memberState, request.GetUserId());
			await context.Save(request);
		}
	}
}
