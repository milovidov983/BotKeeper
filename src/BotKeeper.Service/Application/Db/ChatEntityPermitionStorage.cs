using BotKeeper.Service.Application.Interfaces;
using BotKeeper.Service.Domain.Interfaces;
using BotKeeper.Service.Domain.Models.Entity;
using System;
using System.Threading.Tasks;

namespace BotKeeper.Service.Application.Db {
	internal class ChatEntityPermitionStorage : IPermissionStorage {
		public IStorage storage;
		public IPermission[] GetPermissions() { 
			throw new NotImplementedException()
		}
	}
}