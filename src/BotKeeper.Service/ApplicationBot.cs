namespace BotKeeper.Service {
    using BotKeeper.Service.Core;
    using BotKeeper.Service.Core.interfaces;
    using BotKeeper.Service.Core.Models;
    using BotKeeper.Service.Core.Services;
    using BotKeeper.Service.Core.States;

	using System;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;
    using Telegram.Bot;
	using Telegram.Bot.Args;
	internal class ApplicationBot {

		private readonly TelegramBotClient client;
		private readonly IStorage storage;
		private readonly IServiceFactory serviceFactory;
		public ApplicationBot(
			TelegramBotClient client,
			IStorage storage) {
			this.client = client ?? throw new ArgumentNullException(nameof(client));
			this.storage = storage;
			var sender = new SenderService(client);
			serviceFactory = new ServiceFactory(storage, sender);

		}

		public void Run() {
			client.OnMessage += BotOnMessageReceived;
			client.OnMessageEdited += BotOnMessageReceived;
			client.StartReceiving();
			Console.WriteLine("Bot started...");
			Console.ReadLine();
			client.StopReceiving();
		}

		private async void BotOnMessageReceived(object sender, MessageEventArgs request) {
			var userId = request.Message.From.Id;
			var context = await CreateContext(userId);
			var command = ParseMessage(request.Message.Text);

			await HandleCommand(request, context, command);

		}

		private async Task<Context> CreateContext(long userId) {
			var cachedState = await storage.GetUserState(userId);

			var context = new Context(new GuestState(), serviceFactory);
			if (cachedState.HasResult) {
				return new Context(cachedState.Result, serviceFactory);
			} else {
				var isUserExist = await context.UserService.IsUserExist(userId);
				if (isUserExist) {
					var memberState = new MemberState();
					await storage.SetUserState(userId, memberState);
					await context.TransitionToAsync(memberState, userId);
				}
			}

			return context;
		}

		private static async Task HandleCommand(MessageEventArgs request, Context context, Commands command) {
			switch (command) {
				case Commands.Unknown:
					await context.Handle(request);
					break;
				case Commands.Help:
					await context.ShowHelp(request);
					break;
				case Commands.Login:
					await context.Login(request);
					break;
				case Commands.Register:
					await context.Register(request);
					break;
				default: throw new Exception($"Unknown command {command.ToString()}");
			}
		}

		private Commands ParseMessage(string text) {
			return (text?.Trim()?.ToLowerInvariant()) switch
			{
				@"\help" => Commands.Help,
				@"\login" => Commands.Login,
				@"\register" => Commands.Register,
				_ => Commands.Unknown,
			};
		}
	}
}