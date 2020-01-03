using BotKeeper.Service.Core.Interfaces;
using BotKeeper.Service.Infrastructure;
using System;
using System.Linq;
using System.Reflection;

namespace BotKeeper.Service {
	internal class Settings {
		public const string appId = "BotKeeper.Service";
		public string ApiKey { get; set; }
		public static Settings Instance { get; set; }
		public static ILogger Logger { get; set; }
		static Settings(){
			Instance = new Settings {
				ApiKey = Environment.GetEnvironmentVariable("botapikey")
			};
			Logger = new ConsoleLogger(true);
			var appVersion = GetProductVersion();
			Console.Title = $"{appId} {appVersion}";
		}
		public static string GetProductVersion() {
			var attribute = (AssemblyInformationalVersionAttribute)Assembly
				.GetExecutingAssembly()
				.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), true)
				.Single();
			return attribute.InformationalVersion;
			
		}
	}
}
