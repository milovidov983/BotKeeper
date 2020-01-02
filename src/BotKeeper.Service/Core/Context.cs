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
        public readonly UserService UserService;



        private static readonly object empty = new object();

        public Context(State state, IStorage storage, TelegramBotClient client) {
            this.TransitionTo(state);
            this.storage = storage;
            this.client = client;
            Sender = new Sender(client);
        }

        public object TransitionTo(State state) {
            if(currentState == state) {
                return empty;
            }

            currentState = state;
            currentState.SetContext(this);
            return empty;
        }

        public void InitialState(MessageEventArgs messageEventArgs) {
            currentState.Initial(messageEventArgs);
        }

        public void Help(MessageEventArgs messageEventArgs) {
            currentState.ShowHelp(messageEventArgs);
        }

        public void Request2() {
            //this._state.Handle2();
        }
    }
}
