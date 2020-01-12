using System.Threading.Tasks;

namespace BotKeeper.Service.Core.Interfaces {
	internal interface IContextFactory {
		Task<BotContext> CreateContext(long userId);
	}
}