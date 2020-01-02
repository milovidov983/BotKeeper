using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Interfaces {
	internal interface IPersistedUser {
		string Type { get; set; }
		string Data { get; set; }
		int Id { get; set; }
	}
}
