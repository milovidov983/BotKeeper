using System;

namespace BotKeeper.Service.Core.Factories {
	internal interface IStateFactory {
		State CreateState(string stateName, string requestContext = "");
		State GetState(Type stateType, string requestContext = "")
	}
}