using System.Threading.Tasks;

namespace BotKeeper.Service.Interfaces {
	internal interface IInteractor {
		Task Execute(IMessage message);
	}
}
