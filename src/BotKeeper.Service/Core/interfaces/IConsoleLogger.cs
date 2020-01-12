using BotKeeper.Service.Core.Models;
using System;
using System.Collections.Generic;

namespace BotKeeper.Service.Core.Interfaces {
	internal interface ILogger {
		void Error(Exception ex);
		void Error(Exception e, Dictionary<string, object> requestInfo);
		void Error(Exception ex, string message);
		void Error(Exception ex, IMetrics metrics);
		void Info(string text);
		void Trace(string text);
		void Trace(string text, IMetrics metrics);
		void Trace(string title, Dictionary<string, object> requestInfo);
		void Warn(Exception ex, string message);
		void Warn(string message);

	}
}