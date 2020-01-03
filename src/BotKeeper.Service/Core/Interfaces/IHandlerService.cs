using BotKeeper.Service.Core.Models;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Interfaces {
	internal interface IHandlerService {
		Task HandleCommand(MessageEventArgs request, Context context, Commands command);
	}
}