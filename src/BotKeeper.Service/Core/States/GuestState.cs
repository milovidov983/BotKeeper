using BotKeeper.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.States {
    internal class GuestState : State {
        public override void Handle(MessageEventArgs messageEventArgs) {
            Initial(messageEventArgs);
        }

        public override void Initial(MessageEventArgs messageEventArgs) {
            context.Sender.Send("Welcome Guest!",messageEventArgs);
        }

        public override void Login(MessageEventArgs messageEventArgs) {
            // do nothing
        }

        public override void Register(MessageEventArgs messageEventArgs) {
            var accountIsFree = !context.UserService.IsUserExist(messageEventArgs.Message.From.Id);
            if (accountIsFree) {
                context.Sender.Send("Create a new password(minimum 12 characters):", messageEventArgs);
                context.TransitionTo(new RegisterState(), messageEventArgs.Message.From.Id);
            } else {
                context.TransitionTo(new MemberState(), messageEventArgs.Message.From.Id);
                context.Login(messageEventArgs);
            }
        }

        public override void ShowHelp(MessageEventArgs messageEventArgs) {
            context.Sender.Send("Guest help information...",messageEventArgs);
        }

    }
}
