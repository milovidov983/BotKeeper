using BotKeeper.Service.Core.Helpers;
using BotKeeper.Service.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Services {
    internal class StratagyRepository : IStratagyRepository {
        protected static Dictionary<string, ICommandHandlerStratagy> botHandlersPool = new Dictionary<string, ICommandHandlerStratagy>();

        public StratagyRepository() {
            botHandlersPool = InitCommandHandlerMap();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text">User text message</param>
        /// <returns>An object with a command associated with a user command that he specified in a text message</returns>
        public ICommandHandlerStratagy GetStratagyForCommand(string text) {
            var userCommand = text?.Trim()?.ToLowerInvariant() ?? string.Empty;

            if (botHandlersPool.TryGetValue(userCommand, out var handlerClient)) {
                return handlerClient;
            }

            return new EmptyStratagy();
        }

        /// <summary>
        /// inspired by https://stackoverflow.com/a/6637710/8840033
        /// </summary>
        public static Dictionary<string, ICommandHandlerStratagy> InitCommandHandlerMap() {
            var methods = typeof(BotContext).GetMethods();
            foreach (var handlerMethod in methods) {

                var attrs = handlerMethod.GetCustomAttributes(true);
                foreach (object attr in attrs) {
                    if (attr is CommandAttribute commandAttr) {
                        var userMessageTextCommand = commandAttr.CommandText;
                        var mainHandlerStratagy = new MainStratagy(handlerMethod);

                        botHandlersPool.Add(userMessageTextCommand, mainHandlerStratagy);
                    }
                }

            }

            return botHandlersPool;
        }
    }

    internal class MainStratagy : ICommandHandlerStratagy {
        private readonly MethodInfo botCommandHandler;

        public MainStratagy(MethodInfo botCommandHandler) {
            this.botCommandHandler = botCommandHandler ?? throw new ArgumentNullException(nameof(botCommandHandler));
        }

        public void Execute(BotContext context, MessageEventArgs request) {
            botCommandHandler.Invoke(context, new object[] { request });
        }
    }

    internal class EmptyStratagy : ICommandHandlerStratagy {
        public void Execute(BotContext context, MessageEventArgs request) {
            var metrics = CreateRequestInfo(context, request);
            Ext.SafeRun(async () => await context.Handle(request), metrics);
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
}