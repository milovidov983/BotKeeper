using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Services {
	internal interface IHandlerClient {
		void Execute(Context context, MessageEventArgs request);
	}
}