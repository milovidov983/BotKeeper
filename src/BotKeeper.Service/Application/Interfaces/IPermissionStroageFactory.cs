using BotKeeper.Service.Domain.Interfaces;
using BotKeeper.Service.Domain.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotKeeper.Service.Application.Interfaces {
	public interface IPermissionStorageFactory {
		Task<IPermissionStorage> CreateStorageFor(EntityType entityType);
		object CreateStorage(int userId);
	}
}
