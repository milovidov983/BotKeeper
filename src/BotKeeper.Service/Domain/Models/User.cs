using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Domain.Models {
	public class User<T> {
		public int Id { get; set; }
		public T ExternalSystemId { get; set; }
		public DateTime CreatedOn { get; set; }
		public UserAccess Accesses { get; set; }
	}
}
