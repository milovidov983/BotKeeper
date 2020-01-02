using BotKeeper.Service.Interfaces;
using BotKeeper.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Services.Interactors {

	// Все классы интеракторы должны будут наследоватся от данного класса и их запуск должен происходить путем вызова Execute
	internal abstract class BaseInteractor : IInteractor {
		protected ExternalContext ext;

		public BaseInteractor(ExternalContext ext) {
			this.ext = ext;
		}

		public async Task Execute(IMessage requestMessage) {
			try {
				await ExecuteImpl(requestMessage);
				await OnCommited(requestMessage);
			} catch (InternalException ex) {
				OnError(requestMessage, ex);
			} catch (Exception ex) {
				OnError(requestMessage, new InternalException("See inner exceptions", ex, StatusCodes.InternalError));
			}
		}

		protected abstract Task ExecuteImpl(IMessage requestMessage);

		public virtual Task OnCommited(IMessage requestMessage) => Task.CompletedTask;

		private void OnError(IMessage requestMessage, InternalException ex) {
			if (ex.StatusCode == StatusCodes.InternalError) {
				//  todo override toString in IMessage
				Settings.Logger.Error(ex, requestMessage.ToString());
			} else {
				Settings.Logger.Warn(ex, requestMessage.ToString());
			}
		}
	}
}
