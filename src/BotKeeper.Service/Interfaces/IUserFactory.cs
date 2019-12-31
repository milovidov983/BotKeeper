

namespace BotKeeper.Service.Interfaces {
	using System.Threading.Tasks;
	internal interface IUserFactory {
		Task<IUser> Create(int id);
	}
}
