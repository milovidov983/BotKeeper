using BotKeeper.Service.Core.Factories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.States {
	internal class RegisterState: State {
        private Dictionary<long, int> count = new Dictionary<long, int>();

        public RegisterState(IStateFactory stateFactory) : base(stateFactory) {
        }

        public override async Task Handle(MessageEventArgs request) {
            await Register(request);
        }

        public override async Task ShowHelp(MessageEventArgs request) {
            context.Sender.Send("Register help information", request);
            await Task.Yield();
        }
    }
}
