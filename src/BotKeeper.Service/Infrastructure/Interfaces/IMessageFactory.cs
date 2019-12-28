using BotKeeper.Service.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Infrastructure.Interfaces {
	interface IMessageFactory {
		IMessage Create(MessageEventArgs messageEventArgs);
	}
}
