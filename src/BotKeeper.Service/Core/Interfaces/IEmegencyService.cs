using BotKeeper.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Interfaces {
	internal interface IEmegencyService {
		void SendErrorMessage(MessageEventArgs request, BotException ex);
	}
}
