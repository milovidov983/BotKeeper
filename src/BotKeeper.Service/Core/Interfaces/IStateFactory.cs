using System;

namespace BotKeeper.Service.Core.Factories {
	internal interface IStateFactory {
		State Create(string stateName, string requestContext = "");
		State Create(Type stateType, string requestContext = "");
		State DefaultState { get; }
	}
}