using BotKeeper.Service.Core.Factories;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.States {
    internal class DefaultState : State {
        public DefaultState(IStateFactory stateFactory) : base(stateFactory) {
        }

        public override async Task DefaultAction(MessageEventArgs request) {
            await Initial(request);
        }

        public override async Task Initial(MessageEventArgs request) {
            context.Sender.Send("Welcome Guest!",request);
            await Task.Yield();
        }

        public override async Task Register(MessageEventArgs request) {
            var userId = request.Message.From.Id;

            var accountIsFree = ! (await context.UserService.IsUserExist(request.Message.From.Id));
            if (accountIsFree) {
                bool isCreatedSuccess = await context.UserService.CreateNewAccount(userId);
                if (isCreatedSuccess) {
                    context.Sender.Send("A new account has been created for you", request);
                    var memberState = stateFactory.Create(typeof(MemberState));
                    await context.TransitionToAsync(memberState, request.Message.From.Id);
                } else {
                    context.Sender.Send("Error creating account", request);
                }
            } else {
                context.Sender.Send("DefaultState: You registered yet. Redirect to MemberState", request);
                var memberState = stateFactory.Create(typeof(MemberState));
                await context.TransitionToAsync(memberState, request.Message.From.Id);
                await commands.DefaultAction(request);
            }
        }

        public override async Task ShowHelp(MessageEventArgs request) {
            context.Sender.Send("DefaultState Guest help information...", request);
            await Task.Yield();
        }
    }
}
