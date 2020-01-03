using BotKeeper.Service.Core.Senders;
using BotKeeper.Service.Core.Services;
using BotKeeper.Service.Core.States;
using BotKeeper.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core {
	internal class Context {
        private State currentState = null;
        private readonly IStorage storage;
        private readonly TelegramBotClient client;

        public ISender Sender;
        public UserService UserService;
        public ValidationService ValidationService;


        private static readonly object empty = new object();

        public Context(State state, IStorage storage, TelegramBotClient client) {
            TransitionTo(state);
            this.storage = storage;
            UserService = new UserService(storage);
            Sender = new Sender(client);
        }

        public object TransitionTo(State state, long? userId = null) {
            if(currentState == state) {
                return empty;
            }
            currentState = state;
            if (userId.HasValue) {
                storage.SetUserState(userId.Value, state);
            }
            currentState.SetContext(this);
            return empty;
        }

        public void InitialState(MessageEventArgs messageEventArgs) {
            currentState.Initial(messageEventArgs);
        }

        public void Help(MessageEventArgs messageEventArgs) {
            currentState.ShowHelp(messageEventArgs);
        }

        public void Register(MessageEventArgs messageEventArgs) {
            //currentState.Register(messageEventArgs);
        }       
        public void Handle(MessageEventArgs messageEventArgs) {
            currentState.Handle(messageEventArgs);
        }

        internal void Login(MessageEventArgs request) {
            currentState.Login(request);
        }
    }
}
