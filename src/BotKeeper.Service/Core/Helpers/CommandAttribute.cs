using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Core.Helpers {
	class CommandAttribute : System.Attribute {
        public string CommandText { get; set; }

        public CommandAttribute() { }

        public CommandAttribute(string command) {
            CommandText = command;
        }
    }
}
