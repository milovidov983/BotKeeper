using BotKeeper.Service.Interfaces;
using System;

namespace BotKeeper.Service.Services.Interactors {
	internal class ExternalContext {
		public readonly IUser user;
		public readonly IBotClient client;
		public readonly IInteractonStore interactionStore;
		public readonly IRegistrationService registrationService;

		public ExternalContext(IUser user, IBotClient client, IInteractonStore interactionStore, IRegistrationService registrationService) {
			this.user = user ?? throw new ArgumentNullException(nameof(user));
			this.client = client ?? throw new ArgumentNullException(nameof(client));
			this.interactionStore = interactionStore ?? throw new ArgumentNullException(nameof(interactionStore));
			this.registrationService = registrationService ?? throw new ArgumentNullException(nameof(registrationService));
		}
	}
}