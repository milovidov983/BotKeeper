using BotKeeper.Service.Core.Models;

namespace BotKeeper.Service.Core.Interfaces {
	internal interface IParserService {
		Commands Parse(string text);
	}
}