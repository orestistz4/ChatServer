using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Models
{
	public class MessageModel
	{

		public string Message { get; set; }
		public DateTime Date { get; set; }
		public string Group { get; set; }
		public string Email { get; set; }
		public string Username { get; set; }
	}
}
