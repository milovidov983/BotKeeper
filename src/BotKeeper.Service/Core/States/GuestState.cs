using System.Threading.Tasks;
using Telegram.Bot.Args;
namespace BotKeeper.Service.Core.States {
    internal class GuestState : State {
        public override async Task Handle(MessageEventArgs request) {
            context.Sender.Send("Handle Member!", request);
            await Task.Yield();
        }

        public override async Task Initial(MessageEventArgs request) {
            context.Sender.Send("Welcome member!",request);
            await Task.Yield();
        }

        public override async Task Login(MessageEventArgs request) {
            context.Sender.Send("Type and send your password:", request);
            await context.TransitionToAsync(new LoginState(), request.Message.From.Id);
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
            context.Sender.Send("Member help information...",request);
            await Task.Yield();
        }

        public override Task Yes(MessageEventArgs request) {
            throw new System.NotImplementedException();
        }
    }
}

