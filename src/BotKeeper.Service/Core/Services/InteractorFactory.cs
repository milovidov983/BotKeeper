using BotKeeper.Service.Core.Models.Users;
using BotKeeper.Service.Interfaces;
using BotKeeper.Service.Models.Users;
using BotKeeper.Service.Services.Interactors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Services {
	internal class InteractorFactory : IInteractorFactory {

		protected Dictionary<Type, Func<BaseUser, IBotClient, IInteractor>> interactors 
			= new Dictionary<Type, Func<BaseUser, IBotClient, IInteractor>>();
		private readonly IInteractonStore interactionStore;
		private readonly IRegistrationService registrationService;
		public InteractorFactory(IInteractonStore interactionStore, IRegistrationService registrationService) {
			interactors.Add(typeof(Admin), CreateAdminInteractor);
			interactors.Add(typeof(User), CreateUserInteractor);
			interactors.Add(typeof(UnknownUser), CreateGuestInteractor);
			interactors.Add(typeof(NewUser), CreateNewUserInteractor);

			this.interactionStore = interactionStore;
			this.registrationService = registrationService;
		}

		public IInteractor Create(BaseUser user, IBotClient client) {
			var userType = user.GetType();

			if (interactors.TryGetValue(userType, out var factory)) {
				return factory.Invoke(user, client);
			}
			throw new Exception("Unknown type {orderType}");
		}

		private IInteractor CreateAdminInteractor(BaseUser user, IBotClient client) {
			return new AdminInteractor(user, client, interactionStore);
		}

		private IInteractor CreateUserInteractor(BaseUser user, IBotClient client) {
			return new UserInteractor(user, client, interactionStore);
		}

		private IInteractor CreateGuestInteractor(BaseUser user, IBotClient client) {
			var context = new ExternalContext(user, client, interactionStore, registrationService);
			return new GuestInteractor(context);
		}		
		
		private IInteractor CreateNewUserInteractor(BaseUser user, IBotClient client) {
			var context = new ExternalContext(user, client, interactionStore, registrationService);


			throw new NotImplementedException();
			return new GuestInteractor(context);
		}

	}
}
