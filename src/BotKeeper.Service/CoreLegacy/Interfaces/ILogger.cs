using BotKeeper.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Interfaces {
	internal interface ILogger {
		void Error(Exception ex);
		void Error(Exception ex, string message);
		void Warn(Exception ex, string message);
		void Info(string message);
		void Error(Exception e, Dictionary<string, object> requestInfo);
	}
}
