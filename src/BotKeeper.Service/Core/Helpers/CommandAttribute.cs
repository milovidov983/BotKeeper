namespace BotKeeper.Service.Core.Helpers {
	/// <summary>
	/// Used to mark which command a user should trigger a particular method from the context.
	/// </summary>
	internal class CommandAttribute : System.Attribute {
		public string CommandText { get; set; }

		public CommandAttribute() { }

		public CommandAttribute(string command) {
			CommandText = command;
		}
	}
}