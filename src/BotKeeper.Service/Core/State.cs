using BotKeeper.Service.Core.Factories;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core {
    internal abstract class State {
        protected Context context;
        protected IStateFactory stateFactory;
        public string CurrentState { get {
                return this.GetType().Name;
            } 
        }

        public State(IStateFactory stateFactory) {
            this.stateFactory = stateFactory;
        }

        public void SetContext(Context context) {
            this.context = context;
        }

        public virtual async Task ShowHelp(MessageEventArgs request) {
            await Task.Yield();
            // do nothing, write log
        }
        public virtual async Task Initial(MessageEventArgs request) {
            await Task.Yield();
            // do nothing, write log
        }
        public virtual async Task Login(MessageEventArgs request) {
            await Task.Yield();
            // do nothing, write log
        }
        public virtual async Task Handle(MessageEventArgs request) {
            await Task.Yield();
            // do nothing, write log
        }
        public virtual async Task Register(MessageEventArgs request) {
            await Task.Yield();
            // do nothing, write log
        }
        public virtual async Task Save(MessageEventArgs request) {
            await Task.Yield();
            // do nothing, write log
        }

        public virtual async Task Yes(MessageEventArgs request) {
            await Task.Yield();
            // do nothing, write log
        }
        public virtual async Task No(MessageEventArgs request) {
            await Task.Yield();
            // do nothing, write log
        }
    }
}
