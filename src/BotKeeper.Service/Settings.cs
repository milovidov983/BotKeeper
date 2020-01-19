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
		public string Env { get; set; }

		public bool IsProd { get; set; }

		static Settings() {
			SetTitle();

			Instance = new Settings {
				ApiKey = Environment.GetEnvironmentVariable("botapikey")
							?? EnvException("botapikey", nameof(ApiKey)),
				Env = Environment.GetEnvironmentVariable("appenvironment")
							?? EnvException("appenvironment", nameof(Env)),
			};

			Instance.IsProd = Instance.Env.Equals(AllEnvironments.Production);

			Logger = new ConsoleLogger(true);
		}

		private static void SetTitle() {
			var appVersion = GetProductVersion();
			Console.Title = $"{appId} {appVersion}";
		}

		private static string EnvException(string env, string fieldName) {
			throw new ApplicationException($"Failed to initialize settings parameter {nameof(Settings)}.{fieldName}, "
											+ $"environment variable not found: \"{env}\"!");
		}

		public static string GetProductVersion() {
			var attribute = (AssemblyInformationalVersionAttribute)Assembly
				.GetExecutingAssembly()
				.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), true)
				.Single();
			return attribute.InformationalVersion;
		}


		public class AllEnvironments {
			public const string Test = "test";
			public const string Develop = "develop";
			public const string Production = "production";
		}
	}
}
