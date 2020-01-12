using BotKeeper.Service.Core.Helpers;
using BotKeeper.Service.Core.Interfaces;
using BotKeeper.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using static BotKeeper.Service.Core.Services.HandlerFactory;
using Newtonsoft;
using Newtonsoft.Json;

namespace BotKeeper.Service.Core.Services {
    internal class HandlerFactory : IHandlerFactory {
        //private Dictionary<string, Commands> commandsMap = new Dictionary<string, Commands> {
        //	{ @"\help", Commands.Help },
        //	{ @"\login", Commands.Login },
        //	{ @"\register", Commands.Register }
        //};

        public IHandlerClient GetHandlerForCommand(string text) {
            var userCommand = text?.Trim()?.ToLowerInvariant() ?? string.Empty;

            if (commandHandlersMap.TryGetValue(userCommand, out var handlerClient)) {
                return handlerClient;
            }

            return new EmptyHandler();
        }


        protected static Dictionary<string, IHandlerClient> commandHandlersMap = new Dictionary<string, IHandlerClient>();

        /// <summary>
        /// inspired by the idea https://stackoverflow.com/a/6637710/8840033
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, IHandlerClient> GetCommands() {
            var props = typeof(Context).GetMethods();
            foreach (var handlerMethod in props) {
                var attrs = handlerMethod.GetCustomAttributes(true);
                foreach (object attr in attrs) {
                    if (attr is CommandAttribute commandAttr) {
                        var userCommand = commandAttr.CommandText;
                        var handlerClient = CreateHandlerClient(handlerMethod);
                        commandHandlersMap.Add(userCommand, handlerClient);
                    }
                }
            }
            return commandHandlersMap;
        }

        protected static IHandlerClient CreateHandlerClient(MethodInfo handler) {
            return new HandlerClient(handler);
        }
    }

    internal class HandlerClient : IHandlerClient {
        private readonly MethodInfo handler;

        public HandlerClient(MethodInfo handler) {
            this.handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public void Execute(Context context, MessageEventArgs request) {
            handler.Invoke(context, new object[] { request });
        }

    }

    internal class EmptyHandler : IHandlerClient {
        public void Execute(Context context, MessageEventArgs request) {
            var metrics = CreateRequestInfo(context, request);
            Ext.SafeRun(async () => await context.Handle(request), metrics);
        }

        /// <summary>
        /// Cerate request info for error log.
        /// </summary>
        private Dictionary<string, object> CreateRequestInfo(Context context, MessageEventArgs request) {
            return new Dictionary<string, object> {
                {nameof(context), JsonConvert.SerializeObject(context)},
                {nameof(context), JsonConvert.SerializeObject(request)}
            };
        }
    }
}