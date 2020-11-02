using BotKeeper.Service.Core.Interfaces;
using BotKeeper.Service.Core.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BotKeeper.Service.Core.Factories {
	internal class StateFactory : IStateFactory {
		private readonly Dictionary<Type, AbstractStateDefault> statesInstances
			= new Dictionary<Type, AbstractStateDefault>();

		private readonly Dictionary<string, Type> namesTypesMap
				= new Dictionary<string, Type>();

		private readonly ILogger logger;

		public AbstractStateDefault DefaultState { get; private set; }
		public StateFactory(ILogger logger) {
			var statesInheritors = Assembly.GetAssembly(typeof(AbstractStateDefault))
											.GetTypes()
											.Where(type => type.IsSubclassOf(typeof(AbstractStateDefault)))
											.Where(type => type != typeof(DefaultState));

			foreach (var stateTypeInfo in statesInheritors) {
				var stateInstance = CreateInstanceOf(stateTypeInfo);

				namesTypesMap.Add(stateTypeInfo.Name, stateTypeInfo);
				statesInstances.Add(stateTypeInfo, stateInstance);
			}

			DefaultState = new DefaultState();

			this.logger = logger;
		}

		public AbstractStateDefault Create(string stateName, string requestContext = "") {
			if (namesTypesMap.TryGetValue(stateName ?? string.Empty, out var stateInstance)) {
				return Create(stateInstance, requestContext);
			}

			LogDefaultStateSet(stateName, requestContext);
			return DefaultState;
		}


		public AbstractStateDefault Create(Type stateType, string requestContext = "") {
			if (statesInstances.TryGetValue(stateType ?? typeof(object), out var stateInstance)) {
				return stateInstance;
			}

			LogDefaultStateSet(stateType, requestContext);
			return DefaultState;
		}

		#region Helpers
		private AbstractStateDefault CreateInstanceOf(Type stateTypeInfo) {
			return GetInstance(stateTypeInfo.AssemblyQualifiedName);
		}
		private AbstractStateDefault GetInstance(string fullyQualifiedName) {
			// taken from here: https://stackoverflow.com/a/27119311/8840033
			Type stateType = Type.GetType(fullyQualifiedName);
			return (AbstractStateDefault)Activator.CreateInstance(stateType);
		}
		#endregion

		#region Logging
		private void LogDefaultStateSet(string stateName, string requestContext) {
			var stateType = Type.GetType(stateName);
			LogDefaultStateSet(stateType, requestContext);
		}

		private void LogDefaultStateSet(Type stateType, string requestContext) {
			var mainLogInfo = $"Default state( {DefaultState.GetType()} ) selected. ";
			var additionalLogInfo = $"Unknown state: [ {stateType.Name} <-- ] ";

			logger.Warn(mainLogInfo + additionalLogInfo + requestContext);
		}

		#endregion
	}
}
