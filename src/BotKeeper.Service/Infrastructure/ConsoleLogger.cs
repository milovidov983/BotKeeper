using BotKeeper.Service.Core.Interfaces;
using BotKeeper.Service.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BotKeeper.Service.Infrastructure {

	internal class ConsoleLogger : ILogger {
		private readonly bool logTime;

		private string Time() => logTime
			? $"{DateTime.UtcNow.ToLongTimeString()}"
			: string.Empty;


		public ConsoleLogger(bool logTime) {
			this.logTime = logTime;
		}

		public void Error(Exception ex, string message) {
			Console.WriteLine($"{Time()} Error: {ex.Message}, {message}");
			//todo requestMessage
		}

		public void Error(Exception ex, IMetrics metrics) {
			Console.WriteLine($"{Time()} Error: {ex.Message}, {CreateLoggerInfo(metrics)}");
			//todo requestMessage
		}

		public void Error(Exception ex) {
			Console.WriteLine($"{Time()} Error: {ex.Message}");
		}

		public void Error(Exception e, Dictionary<string, object> requestInfo) {
			Console.WriteLine($"{Time()} Error: {JsonConvert.SerializeObject(requestInfo)}");
		}

		public void Info(string text) {
			Console.WriteLine($"{Time()} Info: {text}");
		}

		public void Trace(string text) {
			Console.WriteLine($"{Time()} Trace: {text}");
		}


		public void Warn(Exception ex, string message) {
			Console.WriteLine($"{Time()} Warn: {ex.Message}, {message}");
		}

		public void Warn(string message) {
			Console.WriteLine($"{Time()} Warn: {message}");
		}

		public void Trace(string title, Dictionary<string, object> requestInfo) {
			Console.WriteLine($"{Time()} Trace: {title}\n{JsonConvert.SerializeObject(requestInfo)}");
		}

		public void Trace(string title, IMetrics metrics = null) {
			Console.WriteLine($"{Time()} Trace: {title}\n{CreateLoggerInfo(metrics)}");
		}



		private static Dictionary<string, object> CreateLoggerInfo(IMetrics metrics) {
			if (metrics is null) {
				return new Dictionary<string, object> {
					{ nameof(metrics), "is null"},
				};
			}

			return new Dictionary<string, object> {
				{ nameof(metrics.CurrentState), metrics.CurrentState},
				{ nameof(metrics.Message), metrics.Message},
				{ nameof(metrics.Request), JsonConvert.SerializeObject(metrics.Request)}
			};
		}
	}
}
