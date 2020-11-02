using BotKeeper.Service.Core.Contexts;
using BotKeeper.Service.Core.Helpers;
using BotKeeper.Service.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Services {
	/// <summary>
	/// Command handler factory, it is able to return the desired handler based on a user request.
	/// </summary>
	internal class CommandHandlerFactory : ICommandHandlerFactory {
		/// <summary>
		/// Collection of handlers for commands.
		/// </summary>
		protected static Dictionary<string, ICommandHandler> commandHandlers = new Dictionary<string, ICommandHandler>();
		protected static Dictionary<string, Dictionary<string, ICommandHandler>> commandHandlers1 = new Dictionary<string, Dictionary<string, ICommandHandler>>();

		/// <summary>
		/// Handler for cases when the command is not found.
		/// </summary>
		private static readonly ICommandHandler defaultHandler = new EmptyHandler();

		public CommandHandlerFactory() {
			commandHandlers = InitCommandHandlerMap();
		}

		/// <summary>
		/// Returns a handler based on a user request.
		/// </summary>
		/// <param name="text">User text message</param>
		/// <returns>An object with a command associated with a user command that he specified in a text message</returns>
		public ICommandHandler CreateHandler(string userCommand) {
			if (commandHandlers.TryGetValue(userCommand, out var commandHandler)) {
				return commandHandler;
			}

			return defaultHandler;
		}

		/// <summary>
		/// inspired by https://stackoverflow.com/a/6637710/8840033
		/// </summary>
		public static Dictionary<string, ICommandHandler> InitCommandHandlerMap() {
			var baseControllerInheritors = Assembly.GetAssembly(typeof(BaseContext))
								.GetTypes()
								.Where(type => type.IsSubclassOf(typeof(BaseContext))
								).ToArray();

			var commandHandlers1 = baseControllerInheritors.Select(controller => {
				var methods = controller.GetMethods();

				var handlers = methods
					.Where(m => m.GetCustomAttributes(true).Any(attr => attr is CommandAttribute))
					.SelectMany(m => m.GetCustomAttributes(true)
							.Where(attr => attr is CommandAttribute)
							.Select(attr => {
								var clientTextCommand = ((CommandAttribute)attr).CommandText;
								var handlerFacade = new CommonHandler(m);

								return (clientTextCommand, handlerFacade);
							})
					)
					.ToDictionary(
						x => x.clientTextCommand, 
						x => x.handlerFacade
					);

				var stateTypeName = controller.GetProperties()
								.Where(x => x.PropertyType == typeof(IBaseState))
								.Select(x => x.Name)
								.First();

				return (stateTypeName, handlers);
			}).ToDictionary(
				x => x.stateTypeName,
				x => x.handlers
			);


			var methods = typeof(DefaultContext).GetMethods();
			foreach (var handlerMethod in methods) {

				var attrs = handlerMethod.GetCustomAttributes(true);
				foreach (object attr in attrs) {
					if (attr is CommandAttribute commandAttr) {
						var userMessageTextCommand = commandAttr.CommandText;
						var mainHandlerStrategy = new CommonHandler(handlerMethod);

						commandHandlers.Add(userMessageTextCommand, mainHandlerStrategy);
					}
				}
			}

			return commandHandlers;
		}

		public ICommandHandler CreateHandlerForCommand(BotContext context, string userTextMessage) {
			throw new NotImplementedException();
		}

		#region Handlers classes

		/// <summary>
		/// Handler for cases when the command exists.
		/// </summary>
		internal class CommonHandler : ICommandHandler, IMethodName {
			private readonly MethodInfo handler;
			public string MethodName {
				get {
					return handler.Name;
				}
			}

			public CommonHandler(MethodInfo handler) {
				this.handler = handler ?? throw new ArgumentNullException(nameof(handler));
			}

			public void Execute(BotContext context, MessageEventArgs request) {
				handler.Invoke(context.Commands, new object[] { request });
			}
		}

		/// <summary>
		/// Handler for cases when the command is not found.
		/// </summary>
		internal class EmptyHandler : ICommandHandler {
			public void Execute(BotContext context, MessageEventArgs request) {
				var metrics = CreateRequestInfo(context, request);
				Ext.SafeRun(async () => await context.Commands.DefaultAction(request), metrics);
			}

			/// <summary>
			/// Cerate request info for error log.
			/// </summary>
			private Dictionary<string, object> CreateRequestInfo(BotContext context, MessageEventArgs request) {
				return new Dictionary<string, object> {
					{nameof(context), JsonConvert.SerializeObject(context)},
					{nameof(request), JsonConvert.SerializeObject(request)}
				};
			}
		}

		#endregion
	}
}