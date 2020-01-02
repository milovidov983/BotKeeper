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
			Context context = CreateContext(request, userId);
			var command = ParseMessage(request.Message.Text);
			HandleCommand(request, context, command);
		}

		private Context CreateContext(MessageEventArgs request, long userId) {
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

			return context;
		}

		private static void HandleCommand(MessageEventArgs request, Context context, Commands command) {
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
			return (text?.Trim()?.ToLowerInvariant()) switch
			{
				@"\help" => Commands.Help,
				@"\login" => Commands.Login,
				@"\register" => Commands.Register,
				_ => Commands.Unknown,
			};
		}

		enum Commands {
			Unknown = 1,
			Help,
			Login,
			Register
		}
	}
}