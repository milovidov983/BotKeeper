using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Core.Models {
	internal class StorageException : Exception {
		public StorageException() {
		}

		public StorageException(string message) : base(message) {
		}

		public StorageException(string message, Exception innerException) : base(message, innerException) {
		}
	}
}
