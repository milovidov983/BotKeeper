using BotKeeper.Service.Core.Helpers;
using BotKeeper.Service.Core.Interfaces;
using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Services {
	internal class SenderService : ISender {

		private readonly TelegramBotClient client;

		public SenderService(TelegramBotClient client) {
			this.client = client ?? throw new ArgumentNullException(nameof(client));
		}
		public void Send(string text, MessageEventArgs request) {
			Ext.SafeRun(async () => await client.SendTextMessageAsync(request.Message.From.Id, text));
		}
	}
}
