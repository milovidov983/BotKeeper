using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service {
	public class Settings {
		public string ApiKey { get; set; }
		public static Settings Instance { get; set; }
		static Settings(){
			
		}
	}
}
