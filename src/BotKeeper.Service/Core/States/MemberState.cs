using BotKeeper.Service.Core;
using BotKeeper.Service.Core.Models;
using BotKeeper.Service.Core.States;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;
namespace BotKeeper.Service.Core.States {
    class MemberState : State {
        public override void Handle(MessageEventArgs messageEventArgs) {
            throw new NotImplementedException();
        }

        public override void Initial(MessageEventArgs messageEventArgs) {
            context.Sender.Send("Welcome member!",messageEventArgs);
        }

        public override void Login(MessageEventArgs messageEventArgs) {
            context.Sender.Send("Type and send your password:", messageEventArgs);
            context.TransitionTo(new LoginState());
        }

        public override void ShowHelp(MessageEventArgs messageEventArgs) {
            context.Sender.Send("Member help information...",messageEventArgs);
        }
    }
}

