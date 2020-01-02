
using System.Threading.Tasks;

namespace BotKeeper.Service.Interfaces {
	internal interface IBotClient {
		Task SendTextMessageAsync(IMessage requestMessage, string response);
	}
}
