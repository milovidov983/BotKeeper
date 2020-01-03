using BotKeeper.Service.Core.interfaces;
using BotKeeper.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Core.Services {
	internal class ParserService : IParserService {
		private Dictionary<string, Commands> commandsMap = new Dictionary<string, Commands> {
			{ @"\help", Commands.Help },
			{ @"\login", Commands.Login },
			{ @"\register", Commands.Register }
		};

		public Commands ParseMessage(string text) {
			var userCommand = text?.Trim()?.ToLowerInvariant() ?? string.Empty;

			if (commandsMap.TryGetValue(userCommand, out var command)) {
				return command;
			}

			return Commands.Unknown;
		}
	}
}
