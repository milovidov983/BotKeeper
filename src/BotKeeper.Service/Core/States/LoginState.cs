using BotKeeper.Service.Core.Helpers;
using BotKeeper.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.States {
    internal class LoginState : State {
        private Dictionary<long, int> count = new Dictionary<long, int>();
        public override void Handle(MessageEventArgs messageEventArgs) {
            Login(messageEventArgs);
        }

        public override void Initial(MessageEventArgs messageEventArgs) {
        }

        public override void Login(MessageEventArgs messageEventArgs) {
            var password = messageEventArgs.Message.Text.Trim();

            var user = context.UserService.Get(messageEventArgs.Message.Chat.Id);
            if(user.Secret == password.Hash()) {
                context.Sender.Send($"Welcome {user.Name}!", messageEventArgs);
                context.TransitionTo(new VerifiedUserState(), messageEventArgs.Message.Chat.Id);
            } else {
                var hasAttempt = AnyAttempts(user);
                if (hasAttempt) {
                    context.Sender.Send("Wrong password, try again: ", messageEventArgs);
                } else {
                    context.TransitionTo(new GuestState(), messageEventArgs.Message.Chat.Id);
                }
            }
        }

        private bool AnyAttempts(User user) {
            return true;
        }

        public override void ShowHelp(MessageEventArgs messageEventArgs) {
            context.Sender.Send("Login help information", messageEventArgs);
        }

        public override void Register(MessageEventArgs messageEventArgs) {
           
        }
    }
}
