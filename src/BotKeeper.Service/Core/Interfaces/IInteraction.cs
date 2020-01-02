using BotKeeper.Service.Models.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Interfaces {
	enum InteractionTypes {
		New = 1,
		Сontinued,
	}
	internal interface IInteraction {
		InteractionTypes Type { get; }

		Task<string> GenerateAnswer(TextMessage message, IUser user);
	}
}
