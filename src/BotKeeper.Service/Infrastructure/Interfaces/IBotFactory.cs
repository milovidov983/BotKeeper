using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;

namespace BotKeeper.Service.Infrastructure.Interfaces {
	interface IBotFactory {
		IBotClient Create(TelegramBotClient client);
	}
}
