using BotKeeper.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.States {
    internal class DefaultState : State {
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

        public override Task No(MessageEventArgs request) {
            throw new NotImplementedException();
        }

        public override async Task Register(MessageEventArgs request) {
            var userId = request.Message.From.Id;

            var accountIsFree = ! (await context.UserService.IsUserExist(request.Message.From.Id));
            if (accountIsFree) {
                bool isCreated = await context.UserService.CreateNewAccount(userId);
                if (isCreated) {
                    context.Sender.Send("A new account has been created for you", request);
                } else {
                    context.Sender.Send("Error creating account", request);
                }
                await context.TransitionToAsync(new MemberState(), request.Message.From.Id);
            } else {
                await context.TransitionToAsync(new GuestState(), request.Message.From.Id);
                await context.Login(request);
            }
        }

        public override async Task Save(MessageEventArgs request) {
            await Task.Yield();
        }

        public override async Task ShowHelp(MessageEventArgs request) {
            context.Sender.Send("Guest help information...",request);
            await Task.Yield();
        }

        public override Task Yes(MessageEventArgs request) {
            throw new NotImplementedException();
        }
    }
}
