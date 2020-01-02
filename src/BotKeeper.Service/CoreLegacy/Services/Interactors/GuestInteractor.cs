using BotKeeper.Service.Interfaces;
using BotKeeper.Service.Models;
using BotKeeper.Service.Models.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Services.Interactors {
	internal class GuestInteractor : BaseInteractor {
		private readonly BaseUser user;
		private readonly IBotClient client;
		private readonly IInteractonFactory interactionFactory;
		private readonly IRegistrationService registrationService;

		public GuestInteractor(ExternalContext context) : base(context) {
			user = context.user;
			client = context.client;
			interactionFactory = context.interactionStore;
			registrationService = context.registrationService;
		}

		protected override async Task ExecuteImpl(IMessage requestMessage) {
			IInteraction interaction = await interactionFactory.Get(user);
			var response = await interaction.GenerateAnswer(requestMessage);


			await client.SendTextMessageAsync(requestMessage, response.Text);
		}
	}
}
