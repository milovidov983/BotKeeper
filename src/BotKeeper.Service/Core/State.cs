using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core {
    internal abstract class State {
        protected Context context;

        public void SetContext(Context context) {
            this.context = context;
        }

        public abstract Task ShowHelp(MessageEventArgs request);
        public abstract Task Initial(MessageEventArgs request);
        public abstract Task Login(MessageEventArgs request);
        public abstract Task Handle(MessageEventArgs request);
        public abstract Task Register(MessageEventArgs request);
    }
}
