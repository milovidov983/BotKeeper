using BotKeeper.Service.Core.Factories;
using BotKeeper.Service.Core.Helpers;
using BotKeeper.Service.Core.Interfaces.UsersInterfaces;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.States {
	internal class SaveState : State {


		private ISaveContextUserService GetUserService() {
			return (ISaveContextUserService) context.UserServiceFactory.CreateUserService(this);
		}

		public override async Task DefaultAction(MessageEventArgs request) {
			await Task.Yield();
			switch (request?.Message?.Type) {
				case Telegram.Bot.Types.Enums.MessageType.Text: {
					var isMessageNotEmpty = ValidateMessage(request.Message.Text);
					if (!isMessageNotEmpty) {
						SendMessageIsEmpty();
						break;
					}
					var userService = GetUserService();
					var key = await userService.SaveUserData(request.GetUserId(), request.Message.Text);

					/// 3) delete source message
					/// 4) return key?
					break;
				}
				case Telegram.Bot.Types.Enums.MessageType.Unknown:
				case Telegram.Bot.Types.Enums.MessageType.Photo:
				case Telegram.Bot.Types.Enums.MessageType.Audio:
				case Telegram.Bot.Types.Enums.MessageType.Video:
				case Telegram.Bot.Types.Enums.MessageType.Voice:
				case Telegram.Bot.Types.Enums.MessageType.Document:
				case Telegram.Bot.Types.Enums.MessageType.Location:
				case Telegram.Bot.Types.Enums.MessageType.Contact:
					SendNotSupportetMessage();
					break;
				default:
					throw new Exception($"Unknown message type {request?.Message?.Type}");
			}
			context.Sender.Send("Save success");
			await context.TransitionToAsync(typeof(MemberState), request.GetUserId());
		}





		#region Helpers 

		private void SendMessageIsEmpty() {
			context.Sender.Send("Sorry, but your message contains no data.");
		}

		private bool ValidateMessage(string text) {
			return text.GetClearedTextMessage() != string.Empty;
		}

		private void SendNotSupportetMessage() {
			context.Sender.Send("Sorry, this type of message is not yet supported.");
		}
		#endregion
	}
}
