using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core {
    internal abstract class State {
        protected Context context;

        public void SetContext(Context context) {
            this.context = context;
        }

        public abstract void ShowHelp(MessageEventArgs messageEventArgs);

        public abstract void Initial(MessageEventArgs messageEventArgs);
        public abstract void Login(MessageEventArgs messageEventArgs);
        public abstract void Handle(MessageEventArgs messageEventArgs);
    }
}
