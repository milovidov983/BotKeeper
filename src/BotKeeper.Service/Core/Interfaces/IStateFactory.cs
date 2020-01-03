namespace BotKeeper.Service.Core.Factories {
	internal interface IStateFactory {
		State CreateState(string stateName, string requestContext = "");
	}
}