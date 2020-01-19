namespace BotKeeper.Service.Core.Interfaces {
	internal interface IStorageResult<T> {
		bool HasResult { get; set; }
		T Result { get; set; }
	}
}