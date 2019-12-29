using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Domain.Interfaces {
	public interface IStorage {
		Task Save<T>(int userId, string key, T value);
	}
}
