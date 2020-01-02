namespace BotKeeper.Service.Interfaces {
	using System.Threading.Tasks;
	internal interface IInteractorFactory {
		IInteractor Create(IUser user, IBotClient client);
	}
}
