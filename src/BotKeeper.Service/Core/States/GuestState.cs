using BotKeeper.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.States {
    internal class GuestState : State {
        public override async Task Handle(MessageEventArgs request) {
            await Initial(request);
        }

        public override async Task Initial(MessageEventArgs request) {
            context.Sender.Send("Welcome Guest!",request);
            await Task.Yield();
        }

        public override async Task Login(MessageEventArgs request) {
            await Task.Yield();
        }

        public override async Task Register(MessageEventArgs request) {
            var accountIsFree = ! (await context.UserService.IsUserExist(request.Message.From.Id));
            if (accountIsFree) {
                context.Sender.Send("Create a new password(minimum 12 characters):", request);
                await context.TransitionToAsync(new RegisterState(), request.Message.From.Id);
            } else {
                await context.TransitionToAsync(new MemberState(), request.Message.From.Id);
                await context.Login(request);
            }
        }

        public override async Task ShowHelp(MessageEventArgs request) {
            context.Sender.Send("Guest help information...",request);
            await Task.Yield();
        }

    }
}
