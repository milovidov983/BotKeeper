using BotKeeper.Service.Infrastructure;
using BotKeeper.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service {
	internal class Settings {
		public string ApiKey { get; set; }
		public static Settings Instance { get; set; }

		public static ILogger Logger { get; set; }
		static Settings(){
			Instance = new Settings {
				ApiKey = Environment.GetEnvironmentVariable("botapikey")
			};
			Logger = new ConsoleLogger();
		}
	}
}
