using BotKeeper.Service.Core;
using BotKeeper.Service.Core.Interfaces;
using BotKeeper.Service.Core.Services;
using Xunit;

namespace BotKeeper.Service.Tests {
	public class CommandHandlerFactoryTests {
		[Fact]
		public void CreateHelpHandler_ReturnHelpHandler() {
			var handlerFactory = new CommandHandlerFactory();

			var handler = (IMethodName)handlerFactory.CreateHandlerForCommand("\\help");
			
			Assert.Equal(nameof(CommandController.ShowHelp), handler.MethodName);
		}
	}
}