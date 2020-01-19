using System;

namespace BotKeeper.Service.Core.Models {
	internal enum UserType {
		Guest = 1,
		Member = 2,
		Admin = 3
	}
	internal abstract class UserBase {
		public long Id { get; set; }
	}

	internal class RegisteredUser : UserBase {
		public UserType Type { get; set; }
		public string Secret { get; set; }
		public string Name { get; set; }
	}

	internal class UserKeeper: UserBase {
		public string UserDefinedKey { get; set; }
	}
}
