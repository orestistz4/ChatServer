using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Models
{
	public class Groups
	{

		[Key]
		public int Id { get; set; }
		public string GroupName { get; set; }
		public string Message { get; set; }
		public string Username { get; set; }


	}
}
