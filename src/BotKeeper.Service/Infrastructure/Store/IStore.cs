using BotKeeper.Service.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Domain.Store {
	public interface IStore {
		Task<int> GetPin(IUser user);
	}
}
