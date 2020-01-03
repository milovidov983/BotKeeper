using BotKeeper.Service.Core.Models;

namespace BotKeeper.Service.Core.interfaces {
	internal interface IParserService {
		Commands ParseMessage(string text);
	}
}