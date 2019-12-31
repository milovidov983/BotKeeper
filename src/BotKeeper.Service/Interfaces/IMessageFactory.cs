namespace BotKeeper.Service.Interfaces {
	using Telegram.Bot.Args;
	internal interface IMessageFactory {
		IMessage Create(MessageEventArgs messageEventArgs);
	}
}
