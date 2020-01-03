using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Core.Interfaces {
	internal interface IStorageResult<T> {
		bool HasResult { get; set; }
		T Result { get; set; }
	}
}