using BotKeeper.Service.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Domain.Models.Entity {
	public abstract class EntityType {
		public virtual IPermission Permission { get; }
	}
}
