using BotKeeper.Service.Core.Interfaces;
using BotKeeper.Service.Core.Models;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Factories {
	internal class TestEmegencyService: IEmegencyService {
		private readonly ISender sender;

		public TestEmegencyService(ISender sender) {
			this.sender = sender;
		}

		public void SendErrorMessage(MessageEventArgs request, BotException ex) {
			sender.Send($"{nameof(TestEmegencyService)} " + ex.Message, request);
		}
	}
}