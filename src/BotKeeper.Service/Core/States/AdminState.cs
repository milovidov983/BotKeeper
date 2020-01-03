using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.States {
    internal class AdminState : State {
        public override async Task Handle(MessageEventArgs request) {
            context.Sender.Send("Handle Admin!", request);
            await Task.Yield();
        }

        public override async Task Initial(MessageEventArgs request) {
            context.Sender.Send("Welcome Admin!",request);
            await Task.Yield();
        }

        public override async Task Login(MessageEventArgs request) {
            context.Sender.Send("You are login yet Admin!", request);
            await Task.Yield();
        }

        public override Task No(MessageEventArgs request) {
            throw new System.NotImplementedException();
        }

        public override async Task Register(MessageEventArgs request) {
            await Task.Yield();
        }

        public override async Task Save(MessageEventArgs request) {
            await Task.Yield();
        }

        public override async Task ShowHelp(MessageEventArgs request) {
            context.Sender.Send("Admin help information...",request);
            await Task.Yield();
        }

        public override Task Yes(MessageEventArgs request) {
            throw new System.NotImplementedException();
        }
    }
}
