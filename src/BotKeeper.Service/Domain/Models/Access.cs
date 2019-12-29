using BotKeeper.Service.Domain.Interfaces;
using System;

namespace BotKeeper.Service.Domain.Models {
	public class UserAccess {
		public DateTime? BannedOn { get; set; }
		public IMember[] RightsCategories { get; set; }
	}
}