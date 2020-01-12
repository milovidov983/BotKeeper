using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core.Helpers {
	public  static class Ext {
		public static void SafeRun(Func<Task> action, Dictionary<string, object> requestInfo = null) {
			Task.Run(async () => {
				try {
					await action.Invoke();
				} catch (Exception e) {
					Settings.Logger.Error(e, requestInfo);
				}
			});
		}

		public static Task StartAsync()


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

		public static long GetUserId(this MessageEventArgs messageEventArgs) {
			return messageEventArgs.Message.From.Id;
		}
	}
}
