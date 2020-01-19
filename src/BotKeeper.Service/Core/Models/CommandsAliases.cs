using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Core.Models {
	/// <summary>
	/// TODO: make class in config (commandsAliases.json)
	/// </summary>
	public class CommandsAliases {
		public static Dictionary<string, string[]> Aliases = new Dictionary<string, string[]>();
		public static Dictionary<string, string> AliaseCommandMap = new Dictionary<string, string>();
		public CommandsAliases() {
			Aliases.Add(AllCommands.Yes, new string[] { @"\y"});
			Aliases.Add(AllCommands.No, new string[] { @"\n" });

			foreach (var item in Aliases) {
				var aliases = item.Value;
				foreach(var aliase in aliases) {
					try {
						AliaseCommandMap.Add(aliase, item.Key);
					} catch (Exception e) {
						throw new InvalidOperationException($"Init {nameof(CommandsAliases)} error. " +
							$"Aliases cannot be repeated: {aliase}", e);
					}
				}
			}
		}
	}
}