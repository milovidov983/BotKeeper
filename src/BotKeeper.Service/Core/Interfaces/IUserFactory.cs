

namespace BotKeeper.Service.Interfaces {
	using System.Threading.Tasks;
	internal interface IUserFactory {
		Task<BaseUser> Create(int id);
	}
}
