using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Domain.Exceptions {
	public class StorageException: Exception {
		public StorageException(string text): base(text) {

		}
	}
}
