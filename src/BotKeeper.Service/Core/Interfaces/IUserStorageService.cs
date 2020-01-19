using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Core.Interfaces {
	internal interface IUserStorageService {
		Task<string> SaveUserData(long userId, string data);
	}
}
