using BotKeeper.Service.Core.Interfaces;
using BotKeeper.Service.Core.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BotKeeper.Service.Core.Factories {
	internal class StateFactory : IStateFactory {
		private Dictionary<string, Func<State>> stateMap;
		private State defaultState = new DefaultState();
		private readonly ILogger logger;

		public StateFactory(ILogger logger) {
			stateMap = Assembly.GetAssembly(typeof(State))
				.GetTypes()
				.Where(type => type.IsSubclassOf(typeof(State)))
				.ToDictionary(
						x => x.Name,
						x => (Func<State>)(() => GetInstance(x.AssemblyQualifiedName))
						);
			this.logger = logger;
		}

		private State GetInstance(string fullyQualifiedName) {
			// taken from here: https://stackoverflow.com/a/27119311/8840033
			Type t = Type.GetType(fullyQualifiedName);
			return (State)Activator.CreateInstance(t);
		}

		public State CreateState(string stateName, string requestContext = "") {
			if (stateMap.TryGetValue(stateName ?? string.Empty, out var stateCreator)) {
				return stateCreator.Invoke();
			}
			logger.Warn($"Unknown state {stateName}. Request context {requestContext}.");
			return defaultState;
		}
	}
}
