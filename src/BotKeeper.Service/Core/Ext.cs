using BotKeeper.Service.Core.Models;
using BotKeeper.Service.Core.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Helpers {
	internal static class Ext {
		public static void SafeRun(Func<Task> action, IMetricsService metricService, string response, MessageEventArgs request) {
			Task.Run(async () => {
				IMetrics metrics = null;
				try {
					await action.Invoke();
				} catch (Exception e) {
					metrics = metricService.CreateMetricsFrom(response, request);
					Settings.Logger.Error(e, metrics);
				}
				metrics ??= metricService.CreateMetricsFrom(response, request);
				Settings.Logger.Trace("Task completed successfully:\n", metrics);
			});
		}

		public static void SafeRun(Func<Task> action, Dictionary<string, object> metrics) {
			Task.Run(async () => {
				try {
					await action.Invoke();
				} catch (Exception e) {
					Settings.Logger.Error(e, metrics);
				}
				Settings.Logger.Trace("Task completed successfully:\n", metrics);
			});
		}


		public static string GetHash(this string value) {
			return ComputeSha256Hash(value);
		}


		static string ComputeSha256Hash(string rawData) {
			using (SHA256 sha256Hash = SHA256.Create()) {
				byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

				StringBuilder builder = new StringBuilder();
				for (int i = 0; i < bytes.Length; i++) {
					builder.Append(bytes[i].ToString("x2"));
				}
				return builder.ToString();
			}
		}

		public static long GetUserId(this MessageEventArgs request) {
			if (request.Message.From.Id == default) {
				throw new BotException($"UserId is not set.", StatusCodes.InvalidRequest);
			}
			return request.Message.From.Id;
		}



		public static string GetClearedTextMessage(this MessageEventArgs request) {
			return request.Message.Text?.Trim()?.ToLowerInvariant() ?? string.Empty;
		}

		public static string GetClearedTextMessage(this string text) {
			return text?.Trim()?.ToLowerInvariant() ?? string.Empty;
		}

		public static string ToJson<T>(this T request) {
			return JsonConvert.SerializeObject(request);
		}
	}
}
