using BotKeeper.Service.Interfaces;
using BotKeeper.Service.Services;
using BotKeeper.Service.Tests.Helpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace BotKeeper.Service.Tests {



	public class InteractorFactoryTests {
		private readonly IInteractonStore mockIInteractonStore = (new Mock<IInteractonStore>()).Object;
		private readonly IRegistrationService mockIRegistrationService = (new Mock<IRegistrationService>()).Object;


		[Fact]
		public void There_Are_As_Many_Interactors_As_There_Are_Users() {
			var allUserTypes = GetAllChildernOf(typeof(BaseUser));
			var service = new InteractorFactoryUnderTest(mockIInteractonStore, mockIRegistrationService);


			var errorMessage = allUserTypes.Count > service.InteractorsCount
				? $"Error: there are more Users={allUserTypes.Count} than Interactors={service.InteractorsCount}. Should be the same."
				: $"Error: there are more Interactors={service.InteractorsCount} than Users={allUserTypes.Count}. Should be the same.";

			Assert.True(allUserTypes.Count == service.InteractorsCount, errorMessage);
		}

		[Fact]
		public void There_Are_As_Many_Interactors_As_There_Are_Users2() {
			var allUserTypes = GetAllChildernOf(typeof(BaseUser));
			var service = new InteractorFactoryUnderTest(mockIInteractonStore, mockIRegistrationService);

			var difference = service.GetAllInteractorTypes()
				.Select(x => x.Name)
				.Except(allUserTypes)
				.ToArray();
		}

		private HashSet<string> GetAllChildernOf(Type baseType) {
			return Assembly.GetAssembly(baseType)
				.GetTypes()
				.Where(type => type.IsSubclassOf(baseType))
				.Select(x => x.Name)
				.ToHashSet();
		}



		#region helper classes
		private class InteractorFactoryUnderTest : InteractorFactory {
			public InteractorFactoryUnderTest(
				IInteractonStore interactionStore,
				IRegistrationService registrationService)
				: base(interactionStore, registrationService) {
			}
			public int InteractorsCount {
				get {
					return interactors.Count;
				}
			}

			public Type[] GetAllInteractorTypes() {
				return interactors.Keys.ToArray();
			}

		}
		#endregion
	}
}
