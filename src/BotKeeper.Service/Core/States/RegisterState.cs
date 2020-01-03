using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.States {
	internal class RegisterState: State {
        private Dictionary<long, int> count = new Dictionary<long, int>();
        public override async Task Handle(MessageEventArgs request) {
            await Register(request);
        }

        public override async Task Initial(MessageEventArgs request) {
            await Task.Yield();
        }

        public override async Task Login(MessageEventArgs request) {
            await Task.Yield();
        }

        public override async Task ShowHelp(MessageEventArgs request) {
            context.Sender.Send("Register help information", request);
            await Task.Yield();
        }

        public override async Task Register(MessageEventArgs request) {
            await Task.Yield();

        }

        public override async Task Save(MessageEventArgs request) {
            await Task.Yield();
        }

        public override Task Yes(MessageEventArgs request) {
            throw new NotImplementedException();
        }

        public override Task No(MessageEventArgs request) {
            throw new NotImplementedException();
        }
    }
}
