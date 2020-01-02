using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.States {
	internal class RegisterState: State {
        private Dictionary<long, int> count = new Dictionary<long, int>();
        public override void Handle(MessageEventArgs messageEventArgs) {
            Register(messageEventArgs);
        }

        public override void Initial(MessageEventArgs messageEventArgs) {
        }

        public override void Login(MessageEventArgs messageEventArgs) {
            //var password = messageEventArgs.Message.Text.Trim();

            //var user = context.UserService.Get(messageEventArgs.Message.From.Id);
            //if (user.Secret == password.Hash()) {
            //    context.Sender.Send($"Welcome {user.Name}!", messageEventArgs);
            //    context.TransitionTo(new VerifiedUserState());
            //} else {
            //    var hasAttempt = AnyAttempts(user);
            //    if (hasAttempt) {
            //        context.Sender.Send("Wrong password, try again: ", messageEventArgs);
            //    } else {
            //        context.TransitionTo(new GuestState());
            //    }
            //}
        }

        public override void ShowHelp(MessageEventArgs messageEventArgs) {
            context.Sender.Send("Register help information", messageEventArgs);
        }

        public override void Register(MessageEventArgs messageEventArgs) {


        }
    }
}
