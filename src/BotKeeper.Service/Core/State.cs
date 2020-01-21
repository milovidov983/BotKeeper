using BotKeeper.Service.Core.Factories;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core {
	internal abstract class State {
		protected BotContext context;
		protected CommandController commands;
		public string CurrentState {
			get {
				return this.GetType().Name;
			}
		}

		public void SetContext(BotContext context) {
			this.context = context;
			commands = context.Commands;
		}

		public virtual async Task ShowHelp(MessageEventArgs request) {
			await Task.Yield();
			// do nothing, write log
		}
		public virtual async Task Initial(MessageEventArgs request) {
			await Task.Yield();
			// do nothing, write log
		}
		public virtual async Task DefaultAction(MessageEventArgs request) {
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

		public virtual async Task Get(MessageEventArgs request) {
			await Task.Yield();
			// do nothing, write log
		}


	}
}
