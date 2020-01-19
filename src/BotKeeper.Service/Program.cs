[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("DynamicProxyGenAssembly2")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("BotKeeper.Service.Tests")]
namespace BotKeeper.Service {
	using BotKeeper.Service.Persistence.Db;
	using System;
	using System.Threading.Tasks;
	using Telegram.Bot;

	class Program {
		static async Task Main(string[] args) {
			Console.OutputEncoding = System.Text.Encoding.UTF8;

			var exitEvent = new System.Threading.ManualResetEvent(false);
			Console.CancelKeyPress += (s, e) => {
				e.Cancel = true;
				exitEvent.Set();
			};

			var logger = Settings.Logger;
			logger.Info("Service starting...");
			try {
				var telegramClient = new TelegramBotClient(Settings.Instance.ApiKey);
				var me = await telegramClient.GetMeAsync();
				logger.Info($"Bot id: {me.Id} name is {me.FirstName}.");

				using var store = new Storage();
				var app = new ApplicationBot(telegramClient, store, Settings.Instance, logger);
				app.Run();


				logger.Info("Service started.");
				exitEvent.WaitOne();

				app.Stop();
				logger.Info("Service stoped.");
			} catch (Exception e) {
				logger.Error(e);
			}
		}
	}
}