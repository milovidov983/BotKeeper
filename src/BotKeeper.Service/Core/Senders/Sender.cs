using BotKeeper.Service.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Senders {
	internal class Sender : ISender {

		private readonly TelegramBotClient client;

		public Sender(TelegramBotClient client) {
			this.client = client ?? throw new ArgumentNullException(nameof(client));
		}
		public void Send(string text, MessageEventArgs request) {
			Ext.SafeRun(async () => await client.SendTextMessageAsync(request.Message.From.Id, text));
		}
	}
}
