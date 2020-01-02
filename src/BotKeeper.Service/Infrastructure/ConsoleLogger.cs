using BotKeeper.Service.Interfaces;
using BotKeeper.Service.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BotKeeper.Service.Infrastructure {
	internal class ConsoleLogger : ILogger {


		public void Error(Exception ex, string message) {
			Console.WriteLine($"ERROR: {ex.Message}, {message}");
			//todo requestMessage
		}

		public void Error(Exception ex) {
			Console.WriteLine($"ERROR: {ex.Message}");
		}

		public void Error(Exception e, Dictionary<string, object> requestInfo) {
			Console.WriteLine($"ERROR: {JsonConvert.SerializeObject(requestInfo)}");
		}

		public void Info(string text) {
			Console.WriteLine(text);
		}

		public void Warn(Exception ex, string message) {
			Console.WriteLine($"WARNING: {ex.Message}, {message}");
			//todo requestMessage
		}
	}
}
