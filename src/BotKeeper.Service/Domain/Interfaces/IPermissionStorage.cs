using BotKeeper.Service.Domain.Interfaces;
using BotKeeper.Service.Domain.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Domain.Interfaces {
	public interface IPermissionStorage {
		Task<IPermission[]> GetAllPermissions(int userId);
	}
}
