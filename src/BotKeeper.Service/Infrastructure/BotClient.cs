using BotKeeper.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace BotKeeper.Service.Infrastructure {
	internal class BotClient  : IBotClient {
		private readonly TelegramBotClient client;

		public BotClient(TelegramBotClient client) {
			this.client = client ?? throw new ArgumentNullException(nameof(client));
		}

		public async Task SendTextMessageAsync(IMessage requestMessage, string response) {
			await client.SendTextMessageAsync(requestMessage.ChatId, response);
		}
	}
}
