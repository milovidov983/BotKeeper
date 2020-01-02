namespace BotKeeper.Service {
    using BotKeeper.Service.Core;
    using BotKeeper.Service.Core.States;
    using BotKeeper.Service.Interfaces;
	using System;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;
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

		private async void BotOnMessageReceived(object sender, MessageEventArgs request) {
			var userId = request.Message.Chat.Id;
			Context context = CreateContext(request, userId);
			var command = ParseMessage(request.Message.Text);
			HandleCommand(request, context, command);
			await Task.Yield();
		}

		private Context CreateContext(MessageEventArgs request, long userId) {
			var cacheResult = storage.GetUserState(userId);

			Context context = new Context(new GuestState(), storage, client);
			if (cacheResult.HasResult) {
				context = new Context(cacheResult.Result, storage, client);
			} else {
				var isUserExist = context.UserService.IsUserExist(userId);
				if (isUserExist) {
					var memberState = new MemberState();
					storage.SetUserState(userId, memberState);
					context.TransitionTo(memberState, userId);
				}
			}

			return context;
		}

		private static void HandleCommand(MessageEventArgs request, Context context, Commands command) {
			switch (command) {
				case Commands.Unknown:
					context.Handle(request);
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