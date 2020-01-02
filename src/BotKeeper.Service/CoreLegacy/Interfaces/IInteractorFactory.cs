namespace BotKeeper.Service.Interfaces {
	using System.Threading.Tasks;
	internal interface IInteractorFactory {
		IInteractor Create(BaseUser user, IBotClient client);
	}
}
