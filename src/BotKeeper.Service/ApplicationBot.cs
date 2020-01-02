namespace BotKeeper.Service {
    using BotKeeper.Service.Core;
    using BotKeeper.Service.Core.States;
    using BotKeeper.Service.Interfaces;
	using System;
    using System.Collections.Concurrent;
    using Telegram.Bot;
	using Telegram.Bot.Args;
	internal class ApplicationBot {

		private readonly TelegramBotClient client;
		private readonly IStorage storage;

		public ApplicationBot(
			TelegramBotClient client,
			IStorage storage) {
			this.client = client ?? throw new ArgumentNullException(nameof(client));
			this.storage = storage;
		}

		public void Run() {
			client.OnMessage += BotOnMessageReceived;
			client.OnMessageEdited += BotOnMessageReceived;
			client.StartReceiving();
			Console.WriteLine("Bot started...");
			Console.ReadLine();
			client.StopReceiving();
		}

		private ConcurrentDictionary<long, State> userStates = new ConcurrentDictionary<long, State>();

		private async void BotOnMessageReceived(object sender, MessageEventArgs request) {
			var userId = request.Message.Chat.Id;
			var isCachedState = userStates.TryGetValue(userId, out var cachedState);

			Context context = new Context(new GuestState(), storage, client);
			if (isCachedState) {
				context = new Context(cachedState, storage, client);
				context.Register(request);
			} else {
				var isUserExist = context.UserService.IsUserExist(userId);
				if (isUserExist) {
					var memberState = new MemberState();
					userStates.TryAdd(userId, memberState);
					context.TransitionTo(memberState);
				}
			}
			var command = ParseMessage(request.Message.Text);
			switch (command) {
				case Commands.Unknown:
					context.Register(request);
					break;
				case Commands.Help:
					context.Help(request);
					break;
				case Commands.Login:
					context.Login(request);
					break;
				case Commands.Register:
					context.Register(request);
					break;
				default: throw new Exception($"Unknown command {command.ToString()}");
			}
		}

		private Commands ParseMessage(string text) {
			throw new NotImplementedException();
		}

		enum Commands {
			Unknown = 1,
			Help,
			Login,
			Register
		}
	}
}