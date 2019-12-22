using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Domain.Models {
	public interface IUser {
		long Id { get; }

		Task ResetPinAttempt();
		Task AddAttempt();
		Task<int> GetPinAttempts();
		Task LockPin();
	}
}
