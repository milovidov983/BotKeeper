using System;

namespace BotKeeper.Service.Core.Models {
	internal class BotException : Exception {
		public StatusCodes StatusCode { get; set; }

		public BotException(string message, StatusCodes statusCode)
			: base(message) {

			StatusCode = statusCode;
		}

		public BotException(string message, Exception innerException, StatusCodes statusCode)
			: base(message, innerException) {

			StatusCode = statusCode;
		}

		public static BotException CreateInternalException(Exception innerException) {
			return new BotException("Internal error, see inner exception.", innerException, StatusCodes.InternalError);
		}
	}
}
