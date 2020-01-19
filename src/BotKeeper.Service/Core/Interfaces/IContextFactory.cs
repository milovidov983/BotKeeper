using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Interfaces {
	internal interface IContextFactory {
		Task<BotContext> CreateContext(MessageEventArgs request);
	}
}