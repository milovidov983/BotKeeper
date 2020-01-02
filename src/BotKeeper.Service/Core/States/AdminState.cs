using BotKeeper.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.States {
    internal class AdminState : State {
        public override void Handle(MessageEventArgs messageEventArgs) {
            throw new NotImplementedException();
        }

        public override void Initial(MessageEventArgs messageEventArgs) {
            context.Sender.Send("Welcome Admin!",messageEventArgs);
        }

        public override void Login(MessageEventArgs messageEventArgs) {
            context.Sender.Send("You are login yet Admin!", messageEventArgs);
        }

        public override void ShowHelp(MessageEventArgs messageEventArgs) {
            context.Sender.Send("Admin help information...",messageEventArgs);
        }

    }
}
