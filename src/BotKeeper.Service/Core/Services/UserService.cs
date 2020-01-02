using BotKeeper.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Core.Services {
    internal class UserService {
        public User Get(long id) {
            return new User { Type = UserType.Guest };
        }

		internal bool IsUserExist(long id) {
			throw new NotImplementedException();
		}
	}
}
