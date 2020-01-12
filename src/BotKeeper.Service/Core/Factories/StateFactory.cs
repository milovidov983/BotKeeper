using BotKeeper.Service.Core.Interfaces;
using BotKeeper.Service.Core.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BotKeeper.Service.Core.Factories {
	internal class StateFactory : IStateFactory {
		private readonly Dictionary<string, Func<State>> nameStateMapping;
		private readonly Dictionary<Type, State> statesPool;
		private readonly State defaultState = new DefaultState();
		private readonly ILogger logger;

		public StateFactory(ILogger logger) {
			var statesInheritors = Assembly.GetAssembly(typeof(State))
				.GetTypes()
				.Where(type => type.IsSubclassOf(typeof(State)));


			foreach(var stateTypeInfo in statesInheritors) {
				var lambdaStateCreator = CreateInstanceOf(stateTypeInfo);
				nameStateMapping.Add(stateTypeInfo.Name, lambdaStateCreator);
				statesPool.Add(stateTypeInfo.DeclaringType, lambdaStateCreator.Invoke());
			}

			this.logger = logger;
		}



		public State CreateState(string stateName, string requestContext = "") {
			if (nameStateMapping.TryGetValue(stateName ?? string.Empty, out var stateCreator)) {
				return stateCreator.Invoke();
			}

			LogDefaultStateSet(stateName, requestContext);
			return defaultState;
		}


		public State GetState(Type stateType, string requestContext = "") {
			if (statesPool.TryGetValue(stateType ?? typeof(object), out var stateInstance)) {
				return stateInstance;
			}

			LogDefaultStateSet(stateType, requestContext);
			return defaultState;
		}

		#region Helpers
		private Func<State> CreateInstanceOf(Type stateTypeInfo) {
			return () => GetInstance(stateTypeInfo.AssemblyQualifiedName);
		}
		private State GetInstance(string fullyQualifiedName) {
			// taken from here: https://stackoverflow.com/a/27119311/8840033
			Type stateType = Type.GetType(fullyQualifiedName);
			return (State)Activator.CreateInstance(stateType);
		}
		#endregion

		#region Logging
		private void LogDefaultStateSet(string stateName, string requestContext) {
			var stateType = Type.GetType(stateName);
			LogDefaultStateSet(stateType, requestContext);
		}

		private void LogDefaultStateSet(Type stateType, string requestContext) {
			var mainLogInfo = $"Default state( {defaultState.GetType()} ) selected. ";
			var additionalLogInfo = $"Unknown state: [ {stateType.Name} <-- ] ";

			logger.Warn(mainLogInfo + additionalLogInfo + requestContext);
		}

		#endregion

	}
}
