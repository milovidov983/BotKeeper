using BotKeeper.Service.Core.Helpers;
using BotKeeper.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.States {
    internal class LoginState : State {
        private Dictionary<long, int> count = new Dictionary<long, int>();
        public override async Task Handle(MessageEventArgs request) {
            await Login(request);
        }

        public override async Task Initial(MessageEventArgs request) {
            await Task.Yield();
        }

        public override async Task Login(MessageEventArgs request) {
            var password = request.Message.Text.Trim();

            var user = await context.UserService.Get(request.Message.From.Id);
            if(user.Secret == password.Hash()) {
                context.Sender.Send($"Welcome {user.Name}!", request);
                await context.TransitionToAsync(new VerifiedUserState(), request.Message.From.Id);
            } else {
                var hasAttempt = AnyAttempts(user);
                if (hasAttempt) {
                    context.Sender.Send("Wrong password, try again: ", request);
                } else {
                    await context.TransitionToAsync(new GuestState(), request.Message.From.Id);
                }
            }
        }

        private bool AnyAttempts(User user) {
            return true;
        }

        public override async Task ShowHelp(MessageEventArgs request) {
            context.Sender.Send("Login help information", request);
            await Task.Yield();
        }

        public override async Task Register(MessageEventArgs request) {
            await Task.Yield();

        }
    }
}
