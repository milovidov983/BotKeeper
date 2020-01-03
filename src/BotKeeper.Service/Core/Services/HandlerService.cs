using BotKeeper.Service.Core.Interfaces;
using BotKeeper.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Services {
	internal class HandlerService : IHandlerService {

		protected Dictionary<Commands, Func<MessageEventArgs, Context, Task>> handlersMap
			= new Dictionary<Commands, Func<MessageEventArgs, Context, Task>>();

		public HandlerService() {
			handlersMap.Add(Commands.Unknown, async (request, context) => await context.Handle(request));
			handlersMap.Add(Commands.Help, async (request, context) => await context.ShowHelp(request));
			handlersMap.Add(Commands.Login, async (request, context) => await context.Login(request));
			handlersMap.Add(Commands.Register, async (request, context) => await context.Register(request));
		}


		public async Task HandleCommand(MessageEventArgs request, Context context, Commands command) {
			if (handlersMap.TryGetValue(command, out var commandHandler)) {
				await commandHandler(request, context);
			} else {
				throw new Exception($"Unknown command {command.ToString()}");
			}
		}
	}
}
