using BotKeeper.Service.Core.Helpers;
using BotKeeper.Service.Core.Interfaces.UsersInterfaces;
using BotKeeper.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.States {
	internal class GetState : State {
		public IGetContextUserService GetUserService() {
			return (IGetContextUserService)
				context.UserServiceFactory.CreateUserService(this);
		}
		public override async Task DefaultAction(MessageEventArgs request) {
			var command = request.GetClearedTextMessage();
			var userId = request.GetUserId();
			switch (command) {
				case GetCommands.Last: {
					var userService = GetUserService();
					var userData = await userService.GetLast(userId);
					if(userData is null) {
						context.Sender.Send("No data saved");
					} else {
						context.Sender.Send(userData);
					}
					break;
				}
				case GetCommands.Keys: {
					break;
				}
				default:
					break;
			}
			await context.TransitionToAsync(typeof(MemberState), request.Message.From.Id);
		}
	}
}
