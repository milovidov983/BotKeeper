using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Core.Models {
    internal enum UserType {
        Guest = 1,
        Member = 2,
        Admin = 3
    }
    internal class User {
        public long Id { get; set; }
        public UserType Type { get; set; }
        public string Secret { get; set; }
    }
}
