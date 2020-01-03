using System.Threading.Tasks;

namespace BotKeeper.Service.Core.Interfaces {
	internal interface IContextFactory {
		Task<Context> CreateContext(long userId);
	}
}