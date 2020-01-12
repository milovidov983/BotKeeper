using System;
using System.ComponentModel;

namespace BotKeeper.Service.Core.Models {
	/// <summary>
	/// Bot processing status
	/// </summary>
	[Flags]
	public enum StatusCodes {
		/// <summary>
		/// Processed
		/// </summary>
		[Description("processed")]
		OK = 0x00,
		/// <summary>
		/// Invalid request
		/// </summary>
		[Description("invalid request")]
		InvalidRequest = 0x01,
		/// <summary>
		/// Not found
		/// </summary>
		[Description("not found")]
		NotFound = 0x02,
		/// <summary>
		/// An internal bot error occurred while processing the request
		/// </summary>
		[Description("an internal bot error occurred while processing the request")]
		InternalError = 0x10000000
	}
}
