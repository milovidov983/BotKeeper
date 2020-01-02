using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Models {
	internal class InternalException : Exception {
		public StatusCodes StatusCode { get; set; }

		public InternalException() {
		}

		public InternalException(string message) : base(message) {
		}

		public InternalException(string message, Exception innerException, StatusCodes statusCode) : base(message, innerException) {
			this.StatusCode = statusCode;
		}
	}
}
