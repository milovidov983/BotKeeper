namespace BotKeeper.Service.Core.Models {
	internal class PersistedUser  {
		public long Id { get; set; }
		public RegisterationData RegisterationData { get; set; }
		public StorageData StorageData { get; set; }
		public PersistedUser() {
			RegisterationData = new RegisterationData();
			StorageData = new StorageData();
		}
	}

	internal class RegisterationData {
		public UserType Type { get; set; }
		public string Secret { get; set; }
		public string Name { get; set; }
	}

	internal class StorageData {
		public string UserDefinedKey { get; set; }
		public string LastKey { get; set; }
	}
}
