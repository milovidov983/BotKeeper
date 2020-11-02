using System;

namespace BotKeeper.Service.Core.Factories {
	internal interface IStateFactory {
		AbstractStateDefault Create(string stateName, string requestContext = "");
		AbstractStateDefault Create(Type stateType, string requestContext = "");
		AbstractStateDefault DefaultState { get; }
	}
}