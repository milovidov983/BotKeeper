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
		private readonly IInteractonStore interactionStore;
		private readonly IRegistrationService registrationService;

		public GuestInteractor(ExternalContext context) : base(context) {
			user = context.user;
			client = context.client;
			interactionStore = context.interactionStore;
			registrationService = context.registrationService;
		}

		protected override async Task ExecuteImpl(IMessage requestMessage) {
			IInteraction interaction = await interactionStore.Get(user, requestMessage.Id);
			var response = string.Empty;

			switch (interaction.Type) {
				case InteractionTypes.New:
					response = @"Hello. I'm bot... type \help for help...";
					break;
				case InteractionTypes.Сontinued:
					response = await GetNextMessage(requestMessage, interaction);
					break;
			}
			await client.SendTextMessageAsync(requestMessage, response);
		}

		//protected Dictionary<Type, Func<IMessage, IBotClient, IInteractor>> messageHandlers
		//		= new Dictionary<Type, Func<IMessage, IBotClient, IInteractor>>();

		private async Task<string> GetNextMessage(IMessage message, IInteraction interaction) {
			return message switch
			{
				TextMessage textMessage => await ComputeCommandAndGetResult(textMessage, interaction),
				_ => throw new NotImplementedException($"Unknown message type {message.GetType()}"),
			};
		}

		private async Task<string> ComputeCommandAndGetResult(TextMessage message, IInteraction interaction) {
			var text = message.Text.Trim().ToLowerInvariant();

			return text switch
			{
				AllInternalCommands.Help => "Help information ...",
				AllInternalCommands.Register => await registrationService.StartRegistrationFor(user),
				_ => await interaction.GenerateAnswer(message, user),
			};
		}
	}
}
