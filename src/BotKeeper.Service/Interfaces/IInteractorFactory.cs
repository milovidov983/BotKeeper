namespace BotKeeper.Service.Interfaces {
	using System.Threading.Tasks;
	internal interface IInteractorFactory {
		Task<IInteractor> Create(IUser user, IBotClient client);
	}
}
