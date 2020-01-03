using BotKeeper.Service.Core.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Core.Services {
	internal class ServiceFactory : IServiceFactory {
		private IStorage storage;
		private IParserService parserService;
		private IUserService userService;
		private ISender sender;

		public IParserService ParserService { get => parserService; }
		public IUserService UserService { get => userService; }
		public IStorage Storage { get => storage; }
		public ISender Sender { get => sender; }

		public ServiceFactory(IStorage storage, ISender sender) {
			this.sender = sender;
			this.storage = storage;
			parserService = new ParserService();
			userService = new UserService(storage);
		}
	}
}
