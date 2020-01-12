using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Core.Helpers {
	internal class StatePool {
		public IDictionary<Type, State> statePool = new Dictionary<Type, State> {

		};

		public StatePool() {
			this.statePool = statePool ?? throw new ArgumentNullException(nameof(statePool));
		}

		public State GetState(Type type) {

		}

	}
}
