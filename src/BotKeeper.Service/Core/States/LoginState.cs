using BotKeeper.Service.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.States {
    internal class LoginState : State {
        public override void Handle(MessageEventArgs messageEventArgs) {
            throw new NotImplementedException();
        }

        public override void Initial(MessageEventArgs messageEventArgs) {
        }

        public override void Login(MessageEventArgs messageEventArgs) {
            var password = messageEventArgs.Message.Text.Trim();

            var user = context.UserService.Get(messageEventArgs.Message.Chat.Id);
            if(user.Secret == password.Hash()) {
                // transite to user type state
            } else {
                context.Sender.Send("Wrong password, try again: ",messageEventArgs);
            }
        }

        public override void ShowHelp(MessageEventArgs messageEventArgs) {
            context.Sender.Send("Login help information", messageEventArgs);
        }

    }
}
