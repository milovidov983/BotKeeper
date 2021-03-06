﻿using BotKeeper.Service.Core.Factories;
using BotKeeper.Service.Core.Helpers;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.States {
	internal class CreateKeyState : State {

		public override async Task DefaultAction(MessageEventArgs request) {
			//await Task.Yield();
			//var key = request.GetClearedTextMessage();

			//if (key == string.Empty) {
			//	context.Sender.Send("CreateKeyState", request);
			//} else {
			//	// check uique
			//}
		}

		public override async Task No(MessageEventArgs request) {
			//var memberState = stateFactory.Create(typeof(MemberState));
			//await context.TransitionToAsync(memberState, request.GetUserId());
			//await commands.Save(request);
		}

		public override async Task Yes(MessageEventArgs request) {
			context.Sender.Send("Send a message to save. A message sent after this will be saved.");
			await context.TransitionToAsync(typeof(SaveState), request.GetUserId());
		}
	}
}
