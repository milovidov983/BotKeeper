using BotKeeper.Service.Core.Factories;
using BotKeeper.Service.Core.Interfaces.UsersInterfaces;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.States {
	internal class DefaultState : State {


		public IRegistrationContextUserService GetUserService( ) {
			return (IRegistrationContextUserService)
				context.UserServiceFactory.CreateUserService(this);
		}

		public override async Task DefaultAction(MessageEventArgs request) {
			await Initial(request);
		}

		public override async Task Initial(MessageEventArgs request) {
			context.Sender.Send("Welcome Guest!", request);
			await Task.Yield();
		}

		public override async Task Register(MessageEventArgs request) {
			var userId = request.Message.From.Id;
			var userService = GetUserService();
			var accountIsFree = !(await userService.IsUserExist(request.Message.From.Id));
			if (accountIsFree) {
				bool isCreatedSuccess = await userService.CreateNewAccount(userId);
				if (isCreatedSuccess) {
					context.Sender.Send("A new account has been created for you");
					await context.TransitionToAsync(typeof(MemberState), request.Message.From.Id);
				} else {
					context.Sender.Send("Error creating account", request);
				}
			} else {
				context.Sender.Send("DefaultState: You registered yet. Redirect to MemberState");
				await context.TransitionToAsync(typeof(MemberState), request.Message.From.Id);
				await commands.DefaultAction(request);
			}
		}

		public override async Task ShowHelp(MessageEventArgs request) {
			context.Sender.Send("DefaultState Guest help information...", request);
			await Task.Yield();
		}
	}
}
