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

        public override void ShowHelp(MessageEventArgs messageEventArgs) {
            context.Sender.Send("Guest help information...",messageEventArgs);
        }

    }
}
