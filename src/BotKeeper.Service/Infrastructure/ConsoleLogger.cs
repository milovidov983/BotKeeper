using BotKeeper.Service.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BotKeeper.Service.Infrastructure {

	internal class ConsoleLogger : ILogger {
		private readonly bool logTime;
		
		private string Time() => logTime 
			? $"{DateTime.UtcNow.ToLongTimeString()} "
			: string.Empty;
		

		public ConsoleLogger(bool logTime) {
			this.logTime = logTime;
		}

		public void Error(Exception ex, string message) {
			Console.WriteLine($"{Time()} Error: {ex.Message}, {message}");
			//todo requestMessage
		}

		public void Error(Exception ex) {
			Console.WriteLine($"{Time()} Error: {ex.Message}");
		}

		public void Error(Exception e, Dictionary<string, object> requestInfo) {
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine($"{Time()} Error: {JsonConvert.SerializeObject(requestInfo)}", Console.ForegroundColor);
		}

		public void Info(string text) {
			Console.WriteLine($"{Time()} Info: {text}", Console.ForegroundColor);
		}

		public void Trace(string text) {
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine($"{Time()} Trace: {text}",	Console.ForegroundColor);
		}
	

		public void Warn(Exception ex, string message) {
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine($"{Time()} Warn: {ex.Message}, {message}", Console.ForegroundColor);
			//todo requestMessage
		}

		public void Warn(string message) {
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine($"{Time()} Warn: {message}", Console.ForegroundColor);
		}

		public void Trace(string title, Dictionary<string, object> requestInfo) {
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine($"{Time()} Trace: {title}\n{JsonConvert.SerializeObject(requestInfo)}", Console.ForegroundColor);
		}
	}
}
