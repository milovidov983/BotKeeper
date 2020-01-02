using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Models {
	[Flags]
	public enum StatusCodes {
		OK = 0x00,
		InvalidRequest = 0x01,
		NotFound = 0x02,
		ExternalError = 0x04,
		InternalError = 0x10000000
	}
}
